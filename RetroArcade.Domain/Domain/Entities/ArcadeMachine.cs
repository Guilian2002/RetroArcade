using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Entities
{
    public class ArcadeMachine
    {
        public Guid Id { get; }
        public string Name { get; set; } = string.Empty;

        public string GameName { get; set; } = string.Empty;

        internal ArcadeMachine() { }

        public ArcadeMachine(Guid id, string name, string gameName)
        {
            Id = id;
            Name = name;
            GameName = gameName;
        }
    }
}
