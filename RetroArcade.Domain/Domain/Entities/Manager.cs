using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Entities
{
    public class Manager
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public Manager(Guid id, string firstname, string lastname, string username, string email)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Email = email;
        }
    }
}
