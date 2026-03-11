using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.RoomFeedbackCommands.Validators
{
    public class DeleteRoomFeedbackCommandValidator : AbstractValidator<DeleteRoomFeedbackCommand>
    {
        public DeleteRoomFeedbackCommandValidator()
        {
            RuleFor(x => x.RoomFeedbackId)
                .NotEmpty().WithMessage("L'identifiant du commentaire est obligatoire.");
        }
    }
}
