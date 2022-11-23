using PureCloudPlatform.Client.V2.Api;

namespace OutboundCall
{
    internal class API
    {
        internal static OutboundApi outboundApi { get; set; }
        internal static NotificationsApi notificationsApi { get; set; }
        internal static AnalyticsApi analyticsApi { get; set; }

        internal static void InitializeApis()
        {
            outboundApi = new OutboundApi();
            notificationsApi = new NotificationsApi();
            analyticsApi = new AnalyticsApi();
        }
    }
}
