using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Queries;

namespace RetroArcade.Domain.Domain.Queries.RoomQueries
{
    public sealed class GetRoomByIdQuery : IQueryDefinition<Room>
    {
        public Guid RoomId { get; }

        public GetRoomByIdQuery(Guid roomId)
        {
            RoomId = roomId;
        }
    }
}
