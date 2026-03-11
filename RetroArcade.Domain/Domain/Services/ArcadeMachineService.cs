using BStorm.Tools.Database;
using RetroArcade.Domain.CustomErrors;
using RetroArcade.Domain.Domain.Commands.ArcadeMachineCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Mappers;
using RetroArcade.Domain.Domain.Queries.ArcadeMachineQueries;
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
    public class ArcadeMachineService : IArcadeMachineRepository
    {
        private readonly DbConnection _dbConnection;

        public ArcadeMachineService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.Open();
        }

        public CqsResult Execute(AddArcadeMachineCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_ArcadeMachine_Insert", true, command);

                if (rows is 0)
                    return Error.Create("Erreur lors de l'insertion");

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult Execute(UpdateArcadeMachineCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_ArcadeMachine_Update", true, parameters: command);

                if (rows == 0)
                    return Errors.ArcadeMachineNotFound;

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult Execute(DeleteArcadeMachineCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_ArcadeMachine_Delete", true, parameters: command);

                if (rows == 0)
                    return Errors.ArcadeMachineNotFound;

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult<IEnumerable<ArcadeMachine>> Execute(GetAllArcadeMachinesQuery query)
        {
            try
            {
                return _dbConnection.ExecuteReader("SP_ArcadeMachine_Get_All",
                    dr => dr.ToArcadeMachineList()).ToList();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
