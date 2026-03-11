using RetroArcade.Domain.Domain.Commands.BookingCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.BookingQueries;
using Tools.Cqs.Commands;
using Tools.Cqs.Queries;

namespace RetroArcade.Domain.Domain.Repositories
{
    public interface IBookingRepository :
        ICommandHandler<AddBookingCommand>, 
        ICommandHandler<UpdateBookingCommand>,
        ICommandHandler<DeleteBookingCommand>,
        IQueryHandler<GetBookingsByRoomQuery, IEnumerable<Booking>>,
        IQueryHandler<GetAllBookingsByManagerQuery, IEnumerable<Booking>>,
        IQueryHandler<GetAllBookingsQuery, IEnumerable<Booking>>,
        IQueryHandler<GetBookingQuery, Booking>
    {
    }
}
