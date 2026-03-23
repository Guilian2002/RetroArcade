using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.RoomCommands.Validators
{
    public class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
    {
        public DeleteRoomCommandValidator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("L'identifiant de la pièce est obligatoire.");
        }
    }
}
