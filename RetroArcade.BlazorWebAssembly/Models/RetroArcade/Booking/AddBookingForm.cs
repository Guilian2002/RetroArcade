using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.Booking
{
    public class AddBookingForm : IValidatableObject
    {
        [Required(ErrorMessage = "LA DATE DE DÉBUT EST OBLIGATOIRE")]
        public DateTime BeginDate { get; set; } = DateTime.Now.AddDays(1).Date.AddHours(10);

        public DateTime EndDate { get; set; }

        [Required]
        [Range(4, 10, ErrorMessage = "GROUPE : 4 À 10 PERSONNES MAX")]
        public int GroupSize { get; set; } = 4;

        [Required]
        public string Status { get; set; } = "OnGoing";

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Guid RoomId { get; set; }

        public TimeSpan OpeningHour { get; set; }
        public TimeSpan ClosingHour { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var calculatedEnd = BeginDate.AddHours(1);

            if (BeginDate < DateTime.Now)
            {
                yield return new ValidationResult("SYSTEM ERROR: IMPOSSIBLE DE RÉSERVER DANS LE PASSÉ", new[] { nameof(BeginDate) });
            }

            var startTime = BeginDate.TimeOfDay;
            var endTime = calculatedEnd.TimeOfDay;

            if (startTime < OpeningHour || endTime > ClosingHour)
            {
                yield return new ValidationResult(
                    $"HORS CRÉNEAU : LA SESSION (1H) DOIT ÊTRE ENTRE {OpeningHour:hh\\:mm} ET {ClosingHour:hh\\:mm}",
                    new[] { nameof(BeginDate) });
            }
        }
    }
}
