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

            OutboundApi outboundApi = new OutboundApi();
            NotificationsApi notificationsApi = new NotificationsApi();

            Channel? channel = notificationsApi.PostNotificationsChannels();
            Notification.SetupNotifications(notificationsApi, channel);
            var campaign = outboundApi.GetOutboundCampaign(ConfigurationManager.AppSettings["campaignId"]);
            ContactLists.SetContactList(outboundApi, campaign, "+4741347834");
            Outbound.StartOutbound("face6225-3995-4ad9-a5af-c6b7f0d21a53", "face6225-3995-4ad9-a5af-c6b7f0d21a53", outboundApi);

            //Websocket.RunWebsocket(channel, outboundApi).Wait();

            Console.ReadLine();
        }
    }
}