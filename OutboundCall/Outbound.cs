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
    internal class Outbound
    {
        internal static void StartOutbound(string conversationId, string agentId)
        {
            OutboundApi outboundApi = new OutboundApi();
            Campaign campaign = outboundApi.GetOutboundCampaign(ConfigurationManager.AppSettings["campaignId"]);
            campaign.Version++;


            outboundApi.PutOutboundCampaign(ConfigurationManager.AppSettings["campaignId"], campaign);
        }
    }
}
