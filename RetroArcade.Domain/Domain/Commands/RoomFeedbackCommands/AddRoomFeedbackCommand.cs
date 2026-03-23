using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.RoomFeedbackCommands
{
    public sealed class AddRoomFeedbackCommand : ICommandDefinition
    {
        public int Stars { get; }
        public string Comment { get; }
	    public Guid AccountId { get; }
        public Guid RoomId { get; }

        public AddRoomFeedbackCommand(int stars, string comment, Guid accountId, Guid roomId)
        {
            Stars = stars;
            Comment = comment;
            AccountId = accountId;
            RoomId = roomId;
        }
    }
}
