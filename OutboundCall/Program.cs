using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PureCloudPlatform.Client.V2.Api;
using PureCloudPlatform.Client.V2.Model;
using System.Configuration;
using System.Net.WebSockets;
using System.Text;

namespace OutboundCall
{
    public class OutboundCall
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting program ... ...");
            // Set configuration file
            //AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", @"..\App.config");




            Configure.RunConfig();

            NotificationsApi notificationsApi = new NotificationsApi();

            Channel? channel = notificationsApi.PostNotificationsChannels();
            Notification.SetupNotifications(notificationsApi, channel);

            Outbound.StartOutbound("","");
            //Websocket.RunWebsocket(channel).Wait();
        }
    }
}