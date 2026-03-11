using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.CategorieCommands.Validators
{
    public class DeleteCategorieCommandValidator : AbstractValidator<DeleteCategorieCommand>
    {
        public DeleteCategorieCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("L'identifiant de la categorie est obligatoire.");
        }
    }
}
