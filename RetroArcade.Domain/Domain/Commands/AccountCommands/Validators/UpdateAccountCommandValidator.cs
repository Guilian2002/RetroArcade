using FluentValidation;
using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.AccountCommands.Validators
{
    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            RuleFor(x => x.AccountId)
                    .NotEmpty().WithMessage("L'identifiant du compte est obligatoire.");

            RuleFor(x => x.Firstname)
                .NotEmpty().WithMessage("Le prénom est requis.");

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage("Le nom est requis.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Le nom d'utilisateur est requis.")
                .MinimumLength(3).WithMessage("Le nom d'utilisateur doit faire au moins 3 caractères.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Le rôle est requis.")
                .IsEnumName(typeof(Role), caseSensitive: false)
                .WithMessage("Le rôle spécifié n'est pas un rôle valide.");
        }
    }
}
