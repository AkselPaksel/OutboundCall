
namespace OutboundCall
{
    internal class Disconnected
    {
        internal static bool IsDisconnected(ResponseSchema.Participant participant)
        {
            if (participant.purpose == "customer")
            {
                if (participant.calls[0].state == "disconnected")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
