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
    public sealed class UpdateAccountCommand : ICommandDefinition
    {
        public Guid AccountId { get; }
        public string Firstname { get; }
        public string Lastname { get; }
        public string Username { get; }
        [EnumDataType(typeof(Role), ErrorMessage = "Ce n\'est pas un role.")]
        public string Role { get; }

        public UpdateAccountCommand(Guid accountId, string firstname, string lastname,
            string username, string role)
        {
            AccountId = accountId;
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Role = role;
        }
    }
}
