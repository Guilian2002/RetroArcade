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
    public sealed class AddBookingCommand : ICommandDefinition
    {
        public DateTime BookingDate { get; }
        public TimeSpan BeginHour { get; }
        public TimeSpan EndHour { get; }
        public int GroupSize { get; }
        [EnumDataType(typeof(Status), ErrorMessage = "Ce n\'est pas un status.")]
        public string Status { get; }
        public Decimal Price { get; }
        public Account? Account { get; }
        public Room? Room { get; }

        public AddBookingCommand(DateTime bookingDate, TimeSpan beginHour, TimeSpan endHour,
            int groupSize, string status, decimal price, Account? account, Room? room)
        {
            BookingDate = bookingDate;
            BeginHour = beginHour;
            EndHour = endHour;
            GroupSize = groupSize;
            Status = status;
            Price = price;
            Account = account;
            Room = room;
        }
    }
}
