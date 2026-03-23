using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Entities
{
    public class Categorie
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Categorie(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
