using BStorm.Tools.Database;
using RetroArcade.Domain.CustomErrors;
using RetroArcade.Domain.Domain.Commands.CategorieCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Mappers;
using RetroArcade.Domain.Domain.Queries.CategorieQueries;
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
    public class CategorieService : ICategorieRepository
    {
        private readonly DbConnection _dbConnection;

        public CategorieService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.Open();
        }

        public CqsResult Execute(AddCategorieCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_Categorie_Insert", true, command);

                if (rows is 0)
                    return Error.Create("Erreur lors de l'insertion");

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult Execute(DeleteCategorieCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_Categorie_Delete", true, parameters: command);

                if (rows == 0)
                    return Errors.CategorieNotFound;

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult<IEnumerable<Categorie>> Execute(GetAllCategoriesQuery query)
        {
            try
            {
                return _dbConnection.ExecuteReader("SP_Categorie_Get_All",
                    dr => dr.ToCategorieList()).ToList();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
