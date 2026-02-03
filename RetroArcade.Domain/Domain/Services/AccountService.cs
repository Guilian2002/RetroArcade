using BStorm.Tools.Database;
using RetroArcade.Domain.CustomErrors;
using RetroArcade.Domain.Domain.Commands.AccountCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Mappers;
using RetroArcade.Domain.Domain.Queries.AccountQueries;
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
    public class AccountService : IAccountRepository
    {
        private readonly DbConnection _dbConnection;

        public AccountService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.Open();
        }

        public CqsResult<Account> Execute(GetAccountByLoginQuery query)
        {
            try
            {
                Account? account = _dbConnection.ExecuteReader("SP_Account_CheckPassword", dr => dr.ToAccountLogin(), true, parameters: query).SingleOrDefault();

                if (account is null)
                    return Errors.AccountNotFound;

                return account;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public CqsResult Execute(AddAccountCommand command)
        {
            try
            {
                int rows = _dbConnection.ExecuteNonQuery("SP_Account_Insert", true, command);

                if (rows is 0)
                    return Error.Create("Erreur lors de l'insertion");

                return CqsResult.Success();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
