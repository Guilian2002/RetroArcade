using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.RoomFeedback
{
    public class RoomFeedbackViewModel
    {
        public Guid Id { get; set; }

        [Range(1, 5)] 
        public int Stars { get; set; }
        public DateTime CommentDate { get; set; }

        [Required] 
        public string Comment { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
