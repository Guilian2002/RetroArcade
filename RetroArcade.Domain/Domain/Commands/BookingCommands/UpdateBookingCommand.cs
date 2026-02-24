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
    public sealed class UpdateBookingCommand : ICommandDefinition
    {
        public Guid BookingId { get; }
        public int GroupSize { get; }

        public UpdateBookingCommand(Guid bookingId, int groupSize,)
        {
            BookingId = bookingId;
            GroupSize = groupSize;
        }
    }
}
