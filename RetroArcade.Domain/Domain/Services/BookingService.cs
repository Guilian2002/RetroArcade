using BStorm.Tools.Database;
using RetroArcade.Domain.CustomErrors;
using RetroArcade.Domain.Domain.Commands.AccountCommands;
using RetroArcade.Domain.Domain.Commands.BookingCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Mappers;
using RetroArcade.Domain.Domain.Queries.AccountQueries;
using RetroArcade.Domain.Domain.Queries.BookingQueries;
using RetroArcade.Domain.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Results;

namespace RetroArcade.Domain.Domain.Services
{
    public class BookingService : IBookingRepository
    {
        private readonly DbConnection _dbConnection;

        public BookingService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.Open();
        }

        public CqsResult Execute(AddBookingCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_Booking_Insert", true, command);

                if (rows is 0)
                    return Error.Create("Erreur lors de l'insertion");

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult<IEnumerable<Booking>> Execute(GetBookingsByRoomQuery query)
        {
            try
            {
                return _dbConnection.ExecuteReader("SP_Booking_Get_By_Room",
                    dr => dr.ToBooking(), true, parameters: query).ToList();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
