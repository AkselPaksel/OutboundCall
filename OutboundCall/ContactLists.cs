using PureCloudPlatform.Client.V2.Api;
using PureCloudPlatform.Client.V2.Model;

namespace OutboundCall
{
    internal class ContactLists
    {
        internal static void SetContactList(OutboundApi outboundApi, Campaign campaign, string phoneNumber)
        {
            var contactListId = campaign.ContactList.Id;

            var contactList = outboundApi.GetOutboundContactlist(contactListId);
            outboundApi.PostOutboundContactlistClear(contactListId);
            List<WritableDialerContact> body = new List<WritableDialerContact>();
            var contact = new WritableDialerContact("Contact", Callable: true, ContactListId: contactListId);
            contact.Data = new Dictionary<string, object>();
            contact.Data.Add("FirstName", "CallContact");
            contact.Data.Add("LastName", "CallContactLastName");
            contact.Data.Add("Phone", phoneNumber);

            body.Add(contact);

            var postResult = outboundApi.PostOutboundContactlistContacts(contactListId, body);
            postResult.ForEach(s => s.Data.ToList().ForEach(b => Console.WriteLine(b.Key + "   " + b.Value)));

            try
            {
                // Add contacts to a contact list.
                List<DialerContact> result = outboundApi.PostOutboundContactlistContacts(contactListId, body);
                result.ForEach(s => s.Data.ToList().ForEach(b => Console.WriteLine(b.Key + "   " + b.Value)));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception when calling Outbound.PostOutboundContactlistContacts: " + e.Message);
            }
        }
    }
}
