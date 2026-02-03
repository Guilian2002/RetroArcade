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
        public string Name { get; set; }
        public TimeSpan OpeningHour { get; set; }
        public TimeSpan ClosingHour { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

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
    }
}
