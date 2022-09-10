using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutboundCall
{
    internal class Ids
    {
        internal static string ConversationId(JObject parsedOutput)
        {
            return parsedOutput["eventBody"]["id"].ToString();
        }
        internal static string GetAgentId(JObject parsedOutput)
        {
            foreach (var participant in (JArray)parsedOutput["eventBody"]["participants"])
            {
                if (participant["purpose"].ToString() == "agent")
                {
                    return participant["userId"].ToString();
                }
            }
            return null;
        }
    }
}
