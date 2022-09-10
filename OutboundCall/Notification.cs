using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PureCloudPlatform.Client.V2.Api;
using PureCloudPlatform.Client.V2.Model;
using System.Configuration;

namespace OutboundCall
{
    internal static class Notification
    {
        public static void SetupNotifications(NotificationsApi notificationsApi, Channel channel)
        {
            notificationsApi.PutNotificationsChannelSubscriptions(
                    channel.Id,
                    new List<ChannelTopic>()
                    {
                        new ChannelTopic()
                        {
                            Id = $"v2.routing.queues.{ConfigurationManager.AppSettings["queueId"]}.conversations"
                        }
                    });
        }
    }
}
