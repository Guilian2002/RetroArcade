using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.ArcadeMachineCommands.Validators
{
    public class UpdateArcadeMachineCommandValidator : AbstractValidator<UpdateArcadeMachineCommand>
    {
        public UpdateArcadeMachineCommandValidator()
        {
            RuleFor(x => x.ArcadeMachineId)
                .NotEmpty().WithMessage("L'identifiant du catalogue de la machine d'arcade est obligatoire.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Le nom est obligatoire.");

            RuleFor(x => x.GameName)
                .NotEmpty().WithMessage("Le nom du jeu est obligatoire.");

            RuleFor(x => x.CategorieId)
                .NotEmpty().WithMessage("L'identifiant de la categorie est obligatoire.");
        }
    }
}
