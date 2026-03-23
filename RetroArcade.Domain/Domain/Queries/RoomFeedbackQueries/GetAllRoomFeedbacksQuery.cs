using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Queries;

namespace RetroArcade.Domain.Domain.Queries.RoomFeedbackQueries
{
    public sealed class GetAllRoomFeedbacksQuery : IQueryDefinition<IEnumerable<RoomFeedback>>
    {
        public Guid AccountId { get; }

        public GetAllRoomFeedbacksQuery(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
