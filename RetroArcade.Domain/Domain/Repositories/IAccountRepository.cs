using RetroArcade.Domain.Domain.Commands.AccountCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.AccountQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;
using Tools.Cqs.Queries;

namespace RetroArcade.Domain.Domain.Repositories
{
    public interface IAccountRepository :
        ICommandHandler<AddAccountCommand>,
        ICommandHandler<UpdateAccountCommand>,
        ICommandHandler<DeleteAccountCommand>,
        IQueryHandler<GetAccountByLoginQuery, Account>,
        IQueryHandler<GetAccountByIdQuery, Account>,
        IQueryHandler<GetAllAccountsQuery, IEnumerable<Account>>
    {
    }
}
