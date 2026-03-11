using FluentValidation;
using RetroArcade.Domain.Domain.Commands.BuildingCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.CategorieCommands.Validators
{
    public class AddCategorieCommandValidator : AbstractValidator<AddCategorieCommand>
    {
        public AddCategorieCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Le nom est obligatoire.");
        }
    }
}
