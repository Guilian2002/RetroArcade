using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Results;

namespace Tools.Cqs.Commands
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommandDefinition
    {
        CqsResult Execute(TCommand command);
    }
}
