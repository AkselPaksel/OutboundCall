using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutboundCall
{
    internal class Conversation
    {
        public bool isDisconnected { get; set; }
        public string agentId { get; set; }
        public string phoneNumber { get; set; }
    }
}
