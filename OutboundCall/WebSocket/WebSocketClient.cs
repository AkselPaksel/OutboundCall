using System.Net.WebSockets;

namespace OutboundCall
{
    internal class WebSocketClient
    {
        internal static ClientWebSocket client { get; set; }
        internal async static Task StartWebsocket()
        {
            client = new ClientWebSocket();
            Uri uri = new Uri(Configure.channel.ConnectUri);
            await client.ConnectAsync(uri, CancellationToken.None);
            Log.Logger.Info("Websocket started");
        }
    }
}
