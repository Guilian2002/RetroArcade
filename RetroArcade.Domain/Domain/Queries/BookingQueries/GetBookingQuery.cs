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
    public sealed class GetBookingQuery : IQueryDefinition<Booking>
    {
        public Guid Id { get; }

        public GetBookingQuery(Guid id)
        {
            Id = id;
        }
    }
}
