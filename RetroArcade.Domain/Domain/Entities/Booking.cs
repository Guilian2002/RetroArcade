using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Entities
{
    public class Booking
    {
        #region Attributes
        public Guid Id { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan BeginHour { get; set; }
        public TimeSpan EndHour { get; set; }
        public int GroupSize { get; set; }
        public Status Status { get; set; }
        public Decimal Price { get; set; }
        public Account? Account { get; set; }
        public Room? Room { get; set; }
        #endregion

        #region CTOR
        public Booking(Guid id, DateTime bookingDate, TimeSpan beginHour, TimeSpan endHour,
            int groupSize, Status status, decimal price, Account? account, Room? room)
        {
            Id = id;
            BookingDate = bookingDate;
            BeginHour = beginHour;
            EndHour = endHour;
            GroupSize = groupSize;
            Status = status;
            Price = price;
            Account = account;
            Room = room;
        }
        #endregion

        #region Methods
        public bool VerifyGroupSizeAllowed() => GroupSize >= 4 && GroupSize <= 10;
        #endregion
    }
}
