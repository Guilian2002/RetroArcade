using BStorm.Tools.Database;
using RetroArcade.Domain.CustomErrors;
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
                Building? building = _dbConnection.ExecuteReader("SP_Building_Get",
                    dr => dr.ToBuildingWithRoom(), true, parameters: query).SingleOrDefault();

                if (building is null)
                    return Errors.BuildingNotFound;

                return building;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
