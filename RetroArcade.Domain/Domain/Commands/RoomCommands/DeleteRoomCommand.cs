using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.RoomCommands
{
    public sealed class DeleteRoomCommand : ICommandDefinition
    {
        public Guid RoomId { get; }

        public DeleteRoomCommand(Guid roomId)
        {
            RoomId = roomId;
        }
    }
}
