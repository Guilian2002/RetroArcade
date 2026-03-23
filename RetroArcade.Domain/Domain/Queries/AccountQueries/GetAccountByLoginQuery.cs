using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Queries;

namespace RetroArcade.Domain.Domain.Queries.AccountQueries
{
    public sealed class GetAccountByLoginQuery : IQueryDefinition<Account>
    {
        public string Email { get; }
        public string Password { get; }

        public GetAccountByLoginQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
