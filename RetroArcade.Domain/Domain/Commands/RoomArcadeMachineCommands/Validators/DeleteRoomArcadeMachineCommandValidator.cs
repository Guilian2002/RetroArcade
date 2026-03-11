using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.RoomArcadeMachineCommands.Validators
{
    public class DeleteRoomArcadeMachineCommandValidator : AbstractValidator<DeleteRoomArcadeMachineCommand>
    {
        public DeleteRoomArcadeMachineCommandValidator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("L'identifiant de la pièce est obligatoire.");

            RuleFor(x => x.ArcadeMachineId)
                .NotEmpty().WithMessage("L'identifiant de la machine d'arcade est obligatoire.");
        }
    }
}
