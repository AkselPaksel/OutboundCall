using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutboundCall
{
    internal class Disconnected
    {
        internal static bool IsDisconnected(JToken participant)
        {
            if (participant["purpose"].ToString() == "customer")
            {
                if (participant["calls"][0]["state"].ToString() == "disconnected")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
