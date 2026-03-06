using RetroArcade.Domain.Domain.Commands.RoomCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.RoomQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;
using Tools.Cqs.Queries;

namespace RetroArcade.Domain.Domain.Repositories
{
    public interface IRoomRepository :
        ICommandHandler<AddRoomCommand>,
        ICommandHandler<UpdateRoomCommand>,
        ICommandHandler<DeleteRoomCommand>,
        IQueryHandler<GetAllRoomsQuery, IEnumerable<Room>>,
        IQueryHandler<GetRoomByIdQuery, Room>
    {
    }
}
