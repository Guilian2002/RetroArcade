using BStorm.Tools.Database;
using RetroArcade.Domain.CustomErrors;
using RetroArcade.Domain.Domain.Commands.BuildingCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Mappers;
using RetroArcade.Domain.Domain.Queries.BuildingQueries;
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
    public class BuildingService : IBuildingRepository
    {
        private readonly DbConnection _dbConnection;

        public BuildingService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.Open();
        }

        public CqsResult<IEnumerable<Building>> Execute(GetAllBuildingsQuery query)
        {
            try
            {
                return _dbConnection.ExecuteReader("SP_Building_Get_All",
                    dr => dr.ToBuilding()).ToList();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult<Building> Execute(GetBuildingByIdQuery query)
        {
            try
            {
                Building? building = null;
                List<Room> roomsList = new List<Room>();

                _dbConnection.ExecuteReader("SP_Building_Get", dr =>
                {
                    if (building == null)
                    {
                        building = dr.MapToBuilding();
                    }

                    var room = dr.MapToRoom();

                    if (room != null)
                    {
                        roomsList.Add(room);
                    }

                    return building;
                }, true, parameters: query).ToList();

                if (building == null) return Errors.BuildingNotFound;

                building.Rooms = roomsList;

                return building;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }


        public CqsResult Execute(AddBuildingCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_Building_Insert", true, command);

                if (rows is 0)
                    return Error.Create("Erreur lors de l'insertion");

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult Execute(UpdateBuildingCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_Building_Update", true, parameters: command);

                if (rows == 0)
                    return Errors.BuildingNotFound;

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult Execute(DeleteBuildingCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_Building_Delete", true, parameters: command);

                if (rows == 0)
                    return Errors.BuildingNotFound;

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
