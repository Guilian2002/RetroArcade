using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.ArcadeMachineCommands
{
    public sealed class UpdateArcadeMachineCommand : ICommandDefinition
    {
        public Guid ArcadeMachineId { get; }
        public string Name { get; }
        public string GameName { get; }
        public Guid CategorieId { get; }

        public UpdateArcadeMachineCommand(Guid arcadeMachineId, string name, string gameName, Guid categorieId)
        {
            ArcadeMachineId = arcadeMachineId;
            Name = name;
            GameName = gameName;
            CategorieId = categorieId;
        }
    }
}
