using FluentValidation;
using RetroArcade.Domain.Domain.Commands.RoomCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.RoomFeedbackCommands.Validators
{
    public class AddRoomFeedbackCommandValidator : AbstractValidator<AddRoomFeedbackCommand>
    {
        public AddRoomFeedbackCommandValidator()
        {
            RuleFor(x => x.Stars)
                .InclusiveBetween(1, 5).WithMessage("Le nombre d'étoiles doit être compris entre 1 et 5.");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Le commentaire est obligatoire.");

            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("L'identifiant du compte est obligatoire.");

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("L'identifiant de la pièce est obligatoire.");
        }
    }
}
