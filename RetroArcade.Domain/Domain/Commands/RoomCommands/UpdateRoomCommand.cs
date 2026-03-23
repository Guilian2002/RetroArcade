using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.RoomCommands
{
    public sealed class UpdateRoomCommand : ICommandDefinition
    {
        public Guid RoomId { get; }
        public string Name { get; }
        public int Number { get; }
        public int MachineCapacity { get; }
        public Decimal Price { get; }
        public Guid BuildingId { get; }

        public UpdateRoomCommand(Guid roomId, string name, int number, int machineCapacity, decimal price, Guid buildingId)
        {
            RoomId = roomId;
            Name = name;
            Number = number;
            MachineCapacity = machineCapacity;
            Price = price;
            BuildingId = buildingId;
        }
    }
}
