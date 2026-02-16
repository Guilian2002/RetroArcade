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
    public sealed class AddAccountCommand : ICommandDefinition
    {
        public string Firstname { get; }
        public string Lastname { get; }
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }
        [EnumDataType(typeof(Role), ErrorMessage = "Ce n\'est pas un role.")]
        public string Role { get; }

        public AddAccountCommand(string firstname, string lastname,
            string username, string email, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(firstname)) throw new ArgumentException("Le prénom est requis.");
            if (string.IsNullOrWhiteSpace(lastname)) throw new ArgumentException("Le nom est requis.");
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Le nom d'utilisateur est requis.");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("L'email est requis.");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Le mot de passe est requis.");
            if (string.IsNullOrWhiteSpace(role)) throw new ArgumentException("Le rôle est requis.");

            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}
