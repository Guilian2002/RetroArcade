using RetroArcade.Domain.Domain.Commands.ArcadeMachineCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.ArcadeMachineQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;
using Tools.Cqs.Queries;

namespace RetroArcade.Domain.Domain.Repositories
{
    public interface IArcadeMachineRepository :
        ICommandHandler<AddArcadeMachineCommand>,
        ICommandHandler<UpdateArcadeMachineCommand>,
        ICommandHandler<DeleteArcadeMachineCommand>,
        IQueryHandler<GetAllArcadeMachinesQuery, IEnumerable<ArcadeMachine>>,
        IQueryHandler<GetArcadeMachineByIdQuery, ArcadeMachine>
    {
    }
}
