using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.AccountCommands
{
    public sealed class DeleteAccountCommand : ICommandDefinition
    {
        public Guid AccountId { get; }

        public DeleteAccountCommand(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
