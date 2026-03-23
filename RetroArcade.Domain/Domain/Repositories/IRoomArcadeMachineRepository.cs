using RetroArcade.Domain.Domain.Commands.BuildingCommands;
using RetroArcade.Domain.Domain.Commands.RoomArcadeMachineCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.BuildingQueries;
using RetroArcade.Domain.Domain.Queries.RoomArcadeMachineQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;
using Tools.Cqs.Queries;

namespace RetroArcade.Domain.Domain.Repositories
{
    public interface IRoomArcadeMachineRepository :
        ICommandHandler<AddRoomArcadeMachineCommand>,
        ICommandHandler<UpdateRoomArcadeMachineCommand>,
        ICommandHandler<DeleteRoomArcadeMachineCommand>,
        IQueryHandler<GetAllRoomArcadeMachinesQuery, IEnumerable<RoomArcadeMachine>>
    {
    }
}
