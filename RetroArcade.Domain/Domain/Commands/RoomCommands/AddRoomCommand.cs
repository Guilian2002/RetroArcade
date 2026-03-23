using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tools.Cqs.Commands;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RetroArcade.Domain.Domain.Commands.RoomCommands
{
    public sealed class AddRoomCommand : ICommandDefinition
    {
        public string Name { get; }
        public int Number { get; }
        public int MachineCapacity { get; }
        public Decimal Price { get; }
        public Guid BuildingId { get; }

        public AddRoomCommand(string name, int number, int machineCapacity, decimal price, Guid buildingId)
        {
            Name = name;
            Number = number;
            MachineCapacity = machineCapacity;
            Price = price;
            BuildingId = buildingId;
        }
    }
}
