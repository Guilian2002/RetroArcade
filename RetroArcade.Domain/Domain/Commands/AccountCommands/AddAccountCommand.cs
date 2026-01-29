using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.AccountCommands
{
    public sealed class AddAccountCommand : ICommandDefinition
    {
        public string Firstname { get; }
        public string Lastname { get; }
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }
        public Role Role { get; }

        internal AddAccountCommand(string firstname, string lastname,
            string username, string email, string password, Role role = Role.User)
        {
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}
