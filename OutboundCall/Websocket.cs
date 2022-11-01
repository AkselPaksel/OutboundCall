using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using PureCloudPlatform.Client.V2.Model;
using Newtonsoft.Json.Linq;
using PureCloudPlatform.Client.V2.Api;
using System.Configuration;

namespace OutboundCall
{
    internal class Websocket
    {
        internal static async Task RunWebsocket(Channel channel, OutboundApi outboundApi, AnalyticsApi analyticsApi)
        {
            using (var client = new ClientWebSocket())
            {
                Uri uri = new Uri(channel.ConnectUri);

                await client.ConnectAsync(uri, CancellationToken.None);

                string rawOutput = "";
                string conversationId = "";
                WebSocketReceiveResult result = null;
                ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);

                // While loop runs forever
                while (client.State == WebSocketState.Open)
                {
                    do
                    {
                        result = await client.ReceiveAsync(bytesReceived, CancellationToken.None).ConfigureAwait(false);
                        if (result.Count > 0)
                        {
                            rawOutput += Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
                        }
                        else
                        {
                            Console.WriteLine("Over");
                        }
                    } while (!result.EndOfMessage);


                    if (!String.IsNullOrEmpty(rawOutput))
                    {
                        var responseSchema = new ResponseSchema.Rootobject();
                        var participants = responseSchema.eventBody.participants;
                        // Check Json is not heartbeat
                        if (participants != null)
                        {
                            try
                            {
                                var customer = participants.Where(p => p.purpose == "customer").First();
                                if (customer.calls[0].state == "disconnected" && !Quarantined.IsQuarantined(customer.address, analyticsApi))
                                {
                                    var agent = participants.Where(p => p.purpose == "agent").First();

                                    var campaign = outboundApi.GetOutboundCampaign(ConfigurationManager.AppSettings["campaignId"]);
                                    ContactLists.SetContactList(outboundApi, campaign, customer.address);
                                    Outbound.StartOutbound(conversationId, agent.id, outboundApi);
                                }
                            }
                            catch (Exception ex) { Console.WriteLine(ex); }
                        }
                        rawOutput = "";
                    }
                }
            }
        }
    }
}