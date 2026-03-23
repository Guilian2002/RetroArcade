using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Entities
{
    public class RoomArcadeMachine
    {
        public string State { get; set; } = "Active";
        public DateTime InstallationDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public Room Room { get; set; } = new Room();

        public ArcadeMachine ArcadeMachine { get; set; } = new ArcadeMachine();

        public RoomArcadeMachine(string state, DateTime installationDate, Room room, ArcadeMachine arcadeMachine)
        {
            State = state;
            InstallationDate = installationDate;
            Room = room;
            ArcadeMachine = arcadeMachine;
        }
    }
}
