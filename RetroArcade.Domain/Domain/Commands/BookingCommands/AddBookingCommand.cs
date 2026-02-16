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
            Account = account ?? throw new ArgumentNullException("Le compte est requis.");
            Room = room ?? throw new ArgumentNullException("La salle est requise.");

            if (beginHour >= endHour)
            {
                throw new ArgumentException("L'heure de début doit etre inférieur à l'heure de fin.");
            }

            if (groupSize < 4 || groupSize > 10)
            {
                throw new ArgumentOutOfRangeException("La taille du groupe doit etre comprise entre 4 et 10 personnes.");
            }

            BookingDate = bookingDate;
            BeginHour = beginHour;
            EndHour = endHour;
            GroupSize = groupSize;
            Status = string.IsNullOrWhiteSpace(status) ? throw new ArgumentException("Le statut est requis.") : status;
            Price = price >= 0 ? price : throw new ArgumentException("Le prix ne peut pas être négatif.");
        }
    }
}
