using FluentValidation;
using RetroArcade.Domain.Domain.Commands.RoomCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.RoomArcadeMachineCommands.Validators
{
    public class AddRoomArcadeMachineCommandValidator : AbstractValidator<AddRoomArcadeMachineCommand>
    {
        public AddRoomArcadeMachineCommandValidator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("L'identifiant de la pièce est obligatoire.");

            RuleFor(x => x.ArcadeMachineId)
                .NotEmpty().WithMessage("L'identifiant de la machine d'arcade est obligatoire.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("L'état de la machine' est obligatoire.");

            RuleFor(x => x.InstallationDate)
                .NotEmpty().WithMessage("La date d'installation est obligatoire.");
        }
    }
}
