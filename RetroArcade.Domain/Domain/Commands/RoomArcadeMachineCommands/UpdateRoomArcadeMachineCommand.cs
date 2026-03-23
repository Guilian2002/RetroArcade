using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.RoomArcadeMachineCommands
{
    public sealed class UpdateRoomArcadeMachineCommand : ICommandDefinition
    {
        public Guid RoomId { get; }
        public Guid ArcadeMachineId { get; }
        public string State { get; }
        public DateTime InstallationDate { get; }

        public UpdateRoomArcadeMachineCommand(Guid roomId, Guid arcadeMachineId, string state, DateTime installationDate)
        {
            RoomId = roomId;
            ArcadeMachineId = arcadeMachineId;
            State = state;
            InstallationDate = installationDate;
        }
    }
}
