using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.RoomFeedbackCommands
{
    public sealed class DeleteRoomFeedbackCommand : ICommandDefinition
    {
        public Guid RoomFeedbackId { get; }

        public DeleteRoomFeedbackCommand(Guid roomFeedbackId)
        {
            RoomFeedbackId = roomFeedbackId;
        }
    }
}
