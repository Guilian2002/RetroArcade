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
        public DateTime BeginDate { get; }
        public DateTime EndDate { get; }
        public int GroupSize { get; }
        [EnumDataType(typeof(Status), ErrorMessage = "Ce n\'est pas un status.")]
        public string Status { get; }
        public Decimal Price { get; }
        public Guid RoomId { get; }
        public Guid AccountId { get; }

        public AddBookingCommand(DateTime beginDate, DateTime endDate,
           int groupSize, string status, decimal price, Guid roomId, Guid accountId)
        {
            BeginDate = beginDate;
            EndDate = endDate;
            GroupSize = groupSize;
            Status = status;
            Price = price;
            RoomId = roomId;
            AccountId = accountId;
        }
    }
}
