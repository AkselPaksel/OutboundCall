using PureCloudPlatform.Client.V2.Model;

namespace OutboundCall
{
    internal class ConversationBody
    {
        public static ConversationQuery CreateConversationBody(string phoneNumber, DateTime quarantineDateFrom, DateTime quarantineDateTo)
        {
            return new ConversationQuery()
            {
                Interval = quarantineDateFrom.Year + "-" + quarantineDateFrom.Month + "-" + quarantineDateFrom.Day + "T23:00:00.000Z/" + quarantineDateTo.Year + "-" + quarantineDateTo.Month + "-" + quarantineDateTo.Day + "T23:00:00.000Z",
                SegmentFilters = new List<SegmentDetailQueryFilter>()
                {
                    new SegmentDetailQueryFilter()
                    {
                        Type = SegmentDetailQueryFilter.TypeEnum.And,
                        Predicates = new List<SegmentDetailQueryPredicate>()
                        {
                            new SegmentDetailQueryPredicate
                            (
                                SegmentDetailQueryPredicate.TypeEnum.Dimension,
                                SegmentDetailQueryPredicate.DimensionEnum.Dnis,
                                Operator: SegmentDetailQueryPredicate.OperatorEnum.Matches,
                                Value: phoneNumber
                            )
                        }
                    }
                }
            };
        }
    }
}
