using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.ArcadeMachineCommands
{
    public sealed class DeleteArcadeMachineCommand : ICommandDefinition
    {
        public Guid ArcadeMachineId { get; }

        public DeleteArcadeMachineCommand(Guid arcadeMachineId)
        {
            ArcadeMachineId = arcadeMachineId;
        }
    }
}
