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
    internal class ContactLists
    {
        internal static string SetContactList(OutboundApi outboundApi, Campaign campaign)
        {
            string contactList;
            WritableDialerContact writableDialerContact = new WritableDialerContact();
            writableDialerContact.Data 
            outboundApi.PostOutboundContactlistClear(campaign.ContactList.Id);
            outboundApi.PostOutboundContactlistContacts(campaign.ContactList.Id,)

            return contactList;
        }
    }
}
