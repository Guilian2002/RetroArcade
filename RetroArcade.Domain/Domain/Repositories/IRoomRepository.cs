using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.RoomQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Queries;

namespace RetroArcade.Domain.Domain.Repositories
{
    public interface IRoomRepository :
        IQueryHandler<GetAllRoomsQuery, IEnumerable<Room>>
    {
    }
}
