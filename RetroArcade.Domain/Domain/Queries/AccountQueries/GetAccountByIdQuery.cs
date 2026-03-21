using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Queries;

namespace RetroArcade.Domain.Domain.Queries.AccountQueries
{
    public sealed class GetAccountByIdQuery : IQueryDefinition<Account>
    {
        public Guid AccountId { get; }

        public GetAccountByIdQuery(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
