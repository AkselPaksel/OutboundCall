using System.Collections.Specialized;
using System.Configuration;

namespace OutboundCall
{
    internal class RelevantOutput
    {
        internal static void EvaluateOutput(ResponseSchema.Rootobject responseSchema)
        {
            List<ContactInformation> contactInformationList = new List<ContactInformation>();
            List<string> quarantinedConversations = new List<string>();

            var participants = responseSchema.eventBody.participants;
            try
            {
                if (participants.Where(p => p.purpose == "agent").Any() && participants.Where(p => p.purpose == "customer").Any())
                {
                    var customer = participants.Where(p => p.purpose == "customer").First();
                    var conversationId = responseSchema.eventBody.id;
                    var agent = participants.Where(p => p.purpose == "agent")?.First();


                    if (customer.calls.First().state == "terminated" &&
                        !quarantinedConversations.Contains(conversationId) &&
                        agent.wrapup != null &&
                        !PhoneNumberQuarantined.IsPhoneNumberQuarantined(customer.address) &&
                        agent.wrapup.code != ConfigurationManager.AppSettings["noSurveyWrapupCode"])
                    {
                        var callSurveyCulture = customer.attributes.CallSurveyCulture;

                        NameValueCollection campaignListSection = (NameValueCollection)ConfigurationManager.GetSection("CampaignId");
                        var campaignId = campaignListSection[callSurveyCulture];
                        ContactInformation newContact = new ContactInformation()
                        {
                            campaign = API.outboundApi.GetOutboundCampaign(campaignId),
                            callSurveyCulture = callSurveyCulture,
                            conversationId = conversationId,
                            contact = Contacts.NewContact(customer.address)
                        };

                        Task.Run(() =>
                        {
                            quarantinedConversations.Add(conversationId);
                            Outbound.StartOutbound(newContact);
                            Thread.Sleep(10000);
                            quarantinedConversations.Remove(conversationId);
                        });
                    }
                }
            }
            catch (Exception ex) { Log.Logger.Info(ex); }
        }
    }
}
    