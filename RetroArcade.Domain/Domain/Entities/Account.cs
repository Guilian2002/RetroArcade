using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        internal Account (Guid id, string firstname, string lastname, 
            string username, string email, string password)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Email = email;
            Password = password;
        }
    }
}
