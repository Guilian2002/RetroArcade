using BStorm.Tools.Database;
using RetroArcade.Domain.CustomErrors;
using RetroArcade.Domain.Domain.Commands.RoomArcadeMachineCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Mappers;
using RetroArcade.Domain.Domain.Queries.RoomArcadeMachineQueries;
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
    public class RoomArcadeMachineService : IRoomArcadeMachineRepository
    {
        private readonly DbConnection _dbConnection;

        public RoomArcadeMachineService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.Open();
        }

        public CqsResult Execute(AddRoomArcadeMachineCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_RoomArcadeMachine_Insert", true, command);

                if (rows is 0)
                    return Error.Create("Erreur lors de l'insertion");

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult Execute(UpdateRoomArcadeMachineCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_RoomArcadeMachine_Update", true, parameters: command);

                if (rows == 0)
                    return Errors.RoomArcadeMachineNotFound;

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult Execute(DeleteRoomArcadeMachineCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_RoomArcadeMachine_Delete", true, parameters: command);

                if (rows == 0)
                    return Errors.RoomArcadeMachineNotFound;

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult<IEnumerable<RoomArcadeMachine>> Execute(GetAllRoomArcadeMachinesQuery query)
        {
            try
            {
                return _dbConnection.ExecuteReader("SP_RoomArcadeMachine_Get_All",
                    dr => dr.ToRoomArcadeMachineList()).ToList();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
