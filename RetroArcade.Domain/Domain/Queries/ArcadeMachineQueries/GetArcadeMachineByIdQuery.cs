using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Queries;

namespace RetroArcade.Domain.Domain.Queries.ArcadeMachineQueries
{
    public sealed class GetArcadeMachineByIdQuery : IQueryDefinition<ArcadeMachine>
    {
        public Guid ArcadeMachineId { get; }

        public GetArcadeMachineByIdQuery(Guid arcadeMachineId)
        {
            ArcadeMachineId = arcadeMachineId;
        }
    }
}
