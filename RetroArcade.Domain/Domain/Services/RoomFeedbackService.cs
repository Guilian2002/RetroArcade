using BStorm.Tools.Database;
using RetroArcade.Domain.CustomErrors;
using RetroArcade.Domain.Domain.Commands.RoomFeedbackCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Mappers;
using RetroArcade.Domain.Domain.Queries.RoomFeedbackQueries;
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
    public class RoomFeedbackService : IRoomFeedbackRepository
    {
        private readonly DbConnection _dbConnection;

        public RoomFeedbackService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.Open();
        }

        public CqsResult Execute(AddRoomFeedbackCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_RoomFeedback_Insert", true, command);

                if (rows is 0)
                    return Error.Create("Erreur lors de l'insertion");

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult Execute(DeleteRoomFeedbackCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_RoomFeedback_Delete", true, parameters: command);

                if (rows == 0)
                    return Errors.RoomFeedbackNotFound;

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult<IEnumerable<RoomFeedback>> Execute(GetAllRoomFeedbacksQuery query)
        {
            try
            {
                return _dbConnection.ExecuteReader("SP_RoomFeedback_Get_All",
                    dr => dr.ToRoomFeedback(), true, parameters: query).ToList();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult<IEnumerable<RoomFeedback>> Execute(GetAllRoomFeedbacksByRoomQuery query)
        {
            try
            {
                return _dbConnection.ExecuteReader("SP_RoomFeedback_Get_ByRoom",
                    dr => dr.ToRoomFeedback(), true, parameters: query).ToList();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
