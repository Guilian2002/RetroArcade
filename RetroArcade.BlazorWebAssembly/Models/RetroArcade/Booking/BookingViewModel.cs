using RetroArcade.BlazorWebAssembly.Models.Authentification;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Enum;
using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Room;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.Booking
{
    public class BookingViewModel
    {
        public Guid Id { get; set; }
        [Required] public DateTime BeginDate { get; set; } = DateTime.Now;
        [Required] public DateTime EndDate { get; set; } = DateTime.Now.AddHours(1);
        [Range(4, 10)] public int GroupSize { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
        public decimal Price { get; set; }

        public RoomViewModel Room { get; set; } = new();

        public AccountViewModel Account { get; set; } = new();
    }
}
