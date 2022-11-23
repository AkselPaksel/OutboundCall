using PureCloudPlatform.Client.V2.Model;

namespace OutboundCall
{
    internal class Outbound
    {
        internal static void StartOutbound(ContactInformation contactInformation)
        {
            try
            {
                var contactListId = contactInformation.campaign.ContactList.Id;

                API.outboundApi.PostOutboundContactlistContacts(
                    contactListId,
                    new List<WritableDialerContact>(){ contactInformation.contact }
                    );
                Log.Logger.Info($"Contact List for SurveyCulture \"{contactInformation.callSurveyCulture}\" updated:\n\n{{\n\t\"contactListId\": {contactListId}\n\t\"phoneNumber\": {contactInformation.contact.Data["Phone"]}\n}}\n");
            }

            catch (Exception ex) 
            {
                Log.Logger.Info(ex);
            }
        }
    }
}
