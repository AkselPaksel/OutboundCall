using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace OutboundCall
{
    internal class WebsocketResponse
    {
        internal static async void RunWebSocket()
        {
            using (var client = WebSocketClient.client)
            {
                // While loop runs forever
                string rawOutput = "";
                ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
                WebSocketReceiveResult result = null;
                while (true)
                {
                    try
                    {
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
                                var responseSchema = JsonSerializer.Deserialize<ResponseSchema.Rootobject>(rawOutput);
                                // Check Json is not heartbeat
                                if (responseSchema?.eventBody.participants != null)
                                {
                                    RelevantOutput.EvaluateOutput(responseSchema);
                                }
                            }
                            rawOutput = "";
                        }
                    }

                    catch (Exception ex) { Console.WriteLine(ex); }
                    Log.Logger.Info("Websocket loop ended");
                }
            }
        }
    }
}