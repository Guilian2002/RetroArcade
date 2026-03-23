using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Queries;

namespace RetroArcade.Domain.Domain.Queries.BookingQueries
{
    public sealed class GetAllBookingsByManagerQuery : IQueryDefinition<IEnumerable<Booking>>
    {
        public Guid AccountId { get; }

        [EnumDataType(typeof(Role), ErrorMessage = "Ce n\'est pas un role.")]
        public string Role { get; }

        public GetAllBookingsByManagerQuery(Guid accountId, string role)
        {
            AccountId = accountId;
            Role = role;
        }
    }
}
