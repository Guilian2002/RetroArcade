using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Entities
{
    public class Room
    {
        public Guid Id { get; }
        public string Name { get; set; } = string.Empty;
        public int Number { get; set; }
        public int MachineCapacity { get; set; }
        public Decimal Price { get; set; }
        public Building Building { get; set; } = new Building();
        public IEnumerable<RoomArcadeMachine> RoomArcadeMachines { get; set; } = new List<RoomArcadeMachine>();
        public IEnumerable<RoomFeedback> RoomFeedbacks { get; set; } = new List<RoomFeedback>();

        internal Room() { }

        public Room(Guid id, string name, int number, Decimal price, Building building, int machineCapacity = 10)
        {
            Id = id;
            Name = name;
            Number = number;
            MachineCapacity = machineCapacity;
            Price = price;
            Building = building;
        }

        public Room(Guid id, string name, int number, Decimal price, 
            Building building, IEnumerable<RoomArcadeMachine> roomArcadeMachines, IEnumerable<RoomFeedback> roomFeedbacks,
            int machineCapacity = 10)
        {
            Id = id;
            Name = name;
            Number = number;
            MachineCapacity = machineCapacity;
            Price = price;
            Building = building;
            RoomArcadeMachines = roomArcadeMachines;
            RoomFeedbacks = roomFeedbacks;
        }
    }
}
