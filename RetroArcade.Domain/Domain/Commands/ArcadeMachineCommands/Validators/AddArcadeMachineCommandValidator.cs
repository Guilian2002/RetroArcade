using FluentValidation;
using RetroArcade.Domain.Domain.Commands.BuildingCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.ArcadeMachineCommands.Validators
{
    public class AddArcadeMachineCommandValidator : AbstractValidator<AddArcadeMachineCommand>
    {
        public AddArcadeMachineCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Le nom est obligatoire.");

            RuleFor(x => x.GameName)
                .NotEmpty().WithMessage("Le nom du jeu est obligatoire.");

            RuleFor(x => x.CategorieId)
                .NotEmpty().WithMessage("L'identifiant de la categorie est obligatoire.");
        }
    }
}
