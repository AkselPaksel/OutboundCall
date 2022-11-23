using PureCloudPlatform.Client.V2.Model;
using System.Configuration;

namespace OutboundCall
{
    internal class PhoneNumberQuarantined
    {
        public static bool IsPhoneNumberQuarantined(string phoneNumber)
        {
            DateTime quarantineDateFrom = DateTime.Now;
            DateTime quarantineDateTo = DateTime.Now;
            List<AnalyticsConversationWithoutAttributes> conversationsList = new List<AnalyticsConversationWithoutAttributes>();
            int quarantinePeriod = int.Parse(ConfigurationManager.AppSettings["quarantinePeriod"]);
            for (int j = 0; quarantinePeriod - 30*j >= quarantinePeriod % 30; j++)
            {
                if (30*(j+1) > quarantinePeriod)
                {
                    quarantineDateFrom = quarantineDateFrom.AddDays(-double.Parse(quarantinePeriod.ToString()));
                    quarantineDateTo = quarantineDateTo.AddDays(-double.Parse((quarantinePeriod - (quarantinePeriod % 30)).ToString()));
                }
                else
                {
                    quarantineDateFrom = quarantineDateFrom.AddDays(-double.Parse((30*(j+1)).ToString()));
                    quarantineDateTo = quarantineDateTo.AddDays(-double.Parse((30*j).ToString()));
                }
                var body = ConversationBody.CreateConversationBody(phoneNumber, quarantineDateFrom, quarantineDateTo);



                try
                {
                    var analyticsQuery = API.analyticsApi.PostAnalyticsConversationsDetailsQuery(body);
                    if (analyticsQuery.Conversations != null)
                    {
                        conversationsList.AddRange(analyticsQuery.Conversations);
                    }
                }
                catch (Exception ex)
                {
                    Log.Logger.Info(ex);
                }
                quarantineDateFrom = DateTime.Now;
                quarantineDateTo = DateTime.Now;
            }


            if (conversationsList != null)
            {
                var externalTagConvList = conversationsList.Where(b => !String.IsNullOrEmpty(b.ExternalTag)).ToList();
                if (externalTagConvList.Count != 0) 
                {
                    return (true);
                }
            }
            return (false);
        }
    }
}