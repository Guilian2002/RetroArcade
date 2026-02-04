using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Entities
{
    public class Building
    {
        public Guid Id { get; }
        public string Name { get; set; } = string.Empty;
        public TimeSpan OpeningHour { get; set; }
        public TimeSpan ClosingHour { get; set; }
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public IEnumerable<Room>? Rooms { get; set; }

        internal Building() { }
        public Building(Guid id, string name, TimeSpan openingHour, TimeSpan closingHour, 
            string street, string number, string postalCode, string city, string country)
        {
            Id = id;
            Name = name;
            OpeningHour = openingHour;
            ClosingHour = closingHour;
            Street = street;
            Number = number;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }
        public Building(Guid id, string name, TimeSpan openingHour, TimeSpan closingHour,
            string street, string number, string postalCode, string city, string country, IEnumerable<Room> rooms)
        {
            Id = id;
            Name = name;
            OpeningHour = openingHour;
            ClosingHour = closingHour;
            Street = street;
            Number = number;
            PostalCode = postalCode;
            City = city;
            Country = country;
            Rooms = rooms;
        }
    }
}
