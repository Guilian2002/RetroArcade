using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.BookingCommands
{
    public sealed class DeleteBookingCommand : ICommandDefinition
    {
        public Guid BookingId { get; }

        public DeleteBookingCommand(Guid bookingId)
        {
            BookingId = bookingId;
        }
    }
}
