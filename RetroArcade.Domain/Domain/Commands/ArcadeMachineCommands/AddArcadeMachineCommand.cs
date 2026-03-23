using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.ArcadeMachineCommands
{
    public sealed class AddArcadeMachineCommand : ICommandDefinition
    {
        public string Name { get; }
        public string GameName { get; }
        public Guid CategorieId { get; }

        public AddArcadeMachineCommand(string name, string gameName, Guid categorieId)
        {
            Name = name;
            GameName = gameName;
            CategorieId = categorieId;
        }
    }
}
