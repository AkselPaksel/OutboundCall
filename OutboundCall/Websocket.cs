using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using PureCloudPlatform.Client.V2.Model;
using Newtonsoft.Json.Linq;

namespace OutboundCall
{
    internal class Websocket
    {
        internal static async Task RunWebsocket(Channel channel)
        {
            try
            {
                using (var client = new ClientWebSocket())
                {
                    Uri uri = new Uri(channel.ConnectUri);

                    await client.ConnectAsync(uri, CancellationToken.None);

                    // Organize Websocket stream into single json
                    string rawOutput = "";
                    string conversationId = "";
                    // key is conversationId. Bool in tuplevalue is if customer has been sent survey. String in tuplevalue is agentId
                    Dictionary<string, (bool, string)> conversationSurveyDict = new Dictionary<string, (bool, string)>();
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
                            var parsedOutput = JObject.Parse(rawOutput);
                            // Check Json is not heartbeat
                            if ((JArray)parsedOutput["eventBody"]["participants"] != null)
                            {
                                conversationId = parsedOutput["eventBody"]["id"].ToString();
                                if (!conversationSurveyDict.ContainsKey(conversationId))
                                {
                                    conversationSurveyDict.Add(conversationId, (false, ""));
                                    // Delete conversationId from Dictionary
                                    new Thread(() =>
                                    {
                                        var tempKey = conversationId;
                                        Thread.CurrentThread.IsBackground = true;
                                        Thread.Sleep(3600 * 1000);
                                        conversationSurveyDict.Remove(tempKey);
                                    }).Start();
                                }
                                else
                                {
                                    if (conversationSurveyDict[conversationId].Item1)
                                    {
                                        rawOutput = "";
                                        continue;
                                    }
                                }

                                try
                                {
                                    foreach (var participant in (JArray)parsedOutput["eventBody"]["participants"])
                                    {
                                        if (participant["purpose"].ToString() == "customer" &&
                                            participant["calls"][0]["state"].ToString() == "connected" && String.IsNullOrEmpty(conversationSurveyDict[conversationId].Item2))
                                        {
                                            foreach (var agentParticipant in (JArray)parsedOutput["eventBody"]["participants"])
                                            {
                                                if (agentParticipant["purpose"].ToString() == "agent")
                                                {
                                                    conversationSurveyDict[conversationId] = (conversationSurveyDict[conversationId].Item1, agentParticipant["userId"].ToString());
                                                }
                                            }
                                        }
                                        if (Disconnected.IsDisconnected(participant))
                                        {
                                            Console.WriteLine("Run outbound" +
                                                "\nConversationId: " + conversationId +
                                                "\nAgentId: " + conversationSurveyDict[conversationId].Item2);
                                            conversationSurveyDict[conversationId] = (true, conversationSurveyDict[conversationId].Item2);
                                        }
                                    }
                                }
                                catch (Exception ex) { Console.WriteLine(ex); }
                            }
                        }
                        rawOutput = "";
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

    }
}
