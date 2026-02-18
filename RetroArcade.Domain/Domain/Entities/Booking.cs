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
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GroupSize { get; set; }
        public Status Status { get; set; }
        public Decimal Price { get; set; }
        public Account? Account { get; set; }
        public Room? Room { get; set; }
        #endregion

        #region CTOR
        public Booking(Guid id, DateTime beginDate, DateTime endDate,
            int groupSize, Status status, decimal price, Account? account, Room? room)
        {
            Id = id;
            BeginDate = beginDate;
            EndDate = endDate;
            GroupSize = groupSize;
            Status = status;
            Price = price;
            Account = account;
            Room = room;
        }
        public Booking(Guid id, DateTime beginDate, DateTime endHour,
            int groupSize, Status status, decimal price, Room? room)
        {
            Id = id;
            BeginDate = beginDate;
            EndDate = endHour;
            GroupSize = groupSize;
            Status = status;
            Price = price;
            Room = room;
        }
        #endregion

        #region Methods
        public bool VerifyGroupSizeAllowed() => GroupSize >= 4 && GroupSize <= 10;
        #endregion
    }
}
