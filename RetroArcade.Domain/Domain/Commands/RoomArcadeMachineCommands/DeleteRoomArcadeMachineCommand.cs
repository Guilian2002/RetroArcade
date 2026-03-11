using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.RoomArcadeMachineCommands
{
    public sealed class DeleteRoomArcadeMachineCommand : ICommandDefinition
    {
        public Guid RoomId { get; }
        public Guid ArcadeMachineId { get; }

        public DeleteRoomArcadeMachineCommand(Guid roomId, Guid arcadeMachineId)
        {
            RoomId = roomId;
            ArcadeMachineId = arcadeMachineId;
        }
    }
}
