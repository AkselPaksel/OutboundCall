using System.Configuration;
using System.Threading.Tasks;
using PureCloudPlatform.Client.V2.Client;
using PureCloudPlatform.Client.V2.Extensions;

namespace OutboundCall
{
    class Configure
    {
        internal static void RunConfig()
        {
            SetEnvironment();
            SetToken();
        }
        private static void SetToken()
        {
            var clientId = ConfigurationManager.AppSettings["clientId"];
            var clientSecret = ConfigurationManager.AppSettings["clientSecret"];

            // Set access token as described in GC documentation
            AuthTokenInfo accessTokenInfo = PureCloudPlatform.Client.V2.Client.Configuration.Default.ApiClient.PostToken(
                clientId,
                clientSecret);
            PureCloudPlatform.Client.V2.Client.Configuration.Default.AccessToken = accessTokenInfo.AccessToken;
        }
        private static void SetEnvironment()
        {
            // Set environment
            PureCloudRegionHosts region = PureCloudRegionHosts.eu_west_1;
            PureCloudPlatform.Client.V2.Client.Configuration.Default.ApiClient.setBasePath(region);
        }
    }
}
