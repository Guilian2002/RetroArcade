using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.BuildingCommands
{
    public sealed class DeleteBuildingCommand : ICommandDefinition
    {
        public Guid BuildingId { get; }

        public DeleteBuildingCommand(Guid buildingId)
        {
            BuildingId = buildingId;
        }
    }
}
