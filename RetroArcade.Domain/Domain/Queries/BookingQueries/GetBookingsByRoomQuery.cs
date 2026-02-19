using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Queries;

namespace RetroArcade.Domain.Domain.Queries.BookingQueries
{
    public sealed class GetBookingsByRoomQuery : IQueryDefinition<IEnumerable<Booking>>
    {
        public Guid RoomId { get; }
        public GetBookingsByRoomQuery(Guid roomId)
        {
            RoomId = roomId;
        }
    }
}
