using PureCloudPlatform.Client.V2.Model;
using System.Collections.Specialized;
using System.Configuration;

namespace OutboundCall
{
    internal class Contacts
    {
        internal static WritableDialerContact NewContact(string phoneNumber)
        {
            WritableDialerContact contact = new WritableDialerContact(Callable: true);
            contact.Data = new Dictionary<string, object>();
            contact.Data.Add("Phone", phoneNumber);
            return contact;
        }
        internal static void ClearContacts()
        {
            NameValueCollection campaignIdSection = (NameValueCollection)ConfigurationManager.GetSection("CampaignId");
            foreach (string campaignIdCulture in campaignIdSection)
            {
                try
                {
                    Log.Logger.Info($"Clear conversation starting for culture: \"{campaignIdCulture}\"");
                    var campaignId = campaignIdSection[campaignIdCulture];

                    var campaign = API.outboundApi.GetOutboundCampaign(campaignId);
                    campaign.CampaignStatus = Campaign.CampaignStatusEnum.Off;
                    API.outboundApi.PutOutboundCampaignAsync(campaignId, campaign).Wait();
                    Log.Logger.Info($"Turned off outbound campaign: {campaignId}");

                    API.outboundApi.PostOutboundContactlistClear(campaign.ContactList.Id);
                    Log.Logger.Info($"Cleared contact list: {campaign.ContactList.Id}");

                    var updatedCampaign = API.outboundApi.GetOutboundCampaign(campaignId);
                    updatedCampaign.CampaignStatus = Campaign.CampaignStatusEnum.On;
                    API.outboundApi.PutOutboundCampaignAsync(campaignId, updatedCampaign).Wait();
                    Log.Logger.Info("Outbound campaign turned back on");
                } catch (Exception ex) { Log.Logger.Info(ex); }
            }
        }
    }
}
