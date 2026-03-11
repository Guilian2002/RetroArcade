using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.ArcadeMachineCommands.Validators
{
    public class DeleteArcadeMachineCommandValidator : AbstractValidator<DeleteArcadeMachineCommand>
    {
        public DeleteArcadeMachineCommandValidator()
        {
            RuleFor(x => x.ArcadeMachineId)
                .NotEmpty().WithMessage("L'identifiant du catalogue de la machine d'arcade est obligatoire.");
        }
    }
}
