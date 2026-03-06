using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.BuildingCommands
{
    public sealed class UpdateBuildingCommand : ICommandDefinition
    {
        public Guid BuildingId { get; }
        public string Name { get; }
        public TimeSpan OpeningHour { get; }
        public TimeSpan ClosingHour { get; }
        public string AddressStreet { get; }
        public string AddressNumber { get; }
        public string PostalCode { get; }
        public string City { get; }
        public string Country { get; }
        public Guid ManagerId { get; }

        public UpdateBuildingCommand(Guid buildingId, string name, TimeSpan openingHour, TimeSpan closingHour, string addressStreet,
                                  string addressNumber, string postalCode, string city, string country, Guid managerId)
        {
            BuildingId = buildingId;
            Name = name;
            OpeningHour = openingHour;
            ClosingHour = closingHour;
            AddressStreet = addressStreet;
            AddressNumber = addressNumber;
            PostalCode = postalCode;
            City = city;
            Country = country;
            ManagerId = managerId;
        }
    }
}
