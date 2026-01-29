using RetroArcade.Domain.Domain.Commands.AccountCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Repositories
{
    public interface IAccountRepository :
        ICommandHandler<AddAccountCommand>
    {
    }
}
