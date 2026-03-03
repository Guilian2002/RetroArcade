using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Entities
{
    public class RoomFeedback
    {
        public Guid Id { get; set; }
        public int Stars { get; set; }
        public DateTime CommentDate { get; set; }
        public string Comment { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public Room? Room { get; set; }

        public RoomFeedback(Guid id, int stars, DateTime commentDate, string comment, string username)
        {
            Id = id;
            Stars = stars;
            CommentDate = commentDate;
            Comment = comment;
            Username = username;
        }
    }
}