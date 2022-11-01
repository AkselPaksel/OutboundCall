using PureCloudPlatform.Client.V2.Api;
using PureCloudPlatform.Client.V2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutboundCall
{
    internal class Quarantined
    {
        public static bool IsQuarantined(string phoneNumber, AnalyticsApi analyticsApi)
        {
            
            ConversationQuery conversationQuery = new ConversationQuery()
            {
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

            var analyticsQuery = analyticsApi.PostAnalyticsConversationsDetailsQuery(conversationQuery);
            var some = analyticsQuery.Conversations.Where(b => !String.IsNullOrEmpty(b.ExternalTag)).ToList();
            some[0].ConversationEnd;
            DateTime? something = some[0].ConversationEnd;
            DateTime.Now;

            return (analyticsQuery.Conversations.Where(b => !String.IsNullOrEmpty(b.ExternalTag)) != null);
        }
    }
}
