using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.BookingCommands
{
    public sealed class DeleteBookingCommand : ICommandDefinition
    {
        public Guid BookingId { get; }
        public Guid AccountId { get; }
        [EnumDataType(typeof(Role), ErrorMessage = "Ce n\'est pas un role.")]
        public string Role { get; }

        public DeleteBookingCommand(Guid bookingId, Guid accountId, string role)
        {
            BookingId = bookingId;
            AccountId = accountId;
            Role = role;
        }
    }
}
