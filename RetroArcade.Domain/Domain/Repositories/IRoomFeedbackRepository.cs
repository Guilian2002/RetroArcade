using RetroArcade.Domain.Domain.Commands.RoomCommands;
using RetroArcade.Domain.Domain.Commands.RoomFeedbackCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.RoomFeedbackQueries;
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
    public interface IRoomFeedbackRepository :
        ICommandHandler<AddRoomFeedbackCommand>,
        ICommandHandler<DeleteRoomFeedbackCommand>,
        IQueryHandler<GetAllRoomFeedbacksQuery, IEnumerable<RoomFeedback>>,
        IQueryHandler<GetAllRoomFeedbacksByRoomQuery, IEnumerable<RoomFeedback>>
    {
    }
}
