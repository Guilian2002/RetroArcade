using FluentValidation;
using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.AccountCommands.Validators
{
    public class AddAccountCommandValidator : AbstractValidator<AddAccountCommand>
    {
        public AddAccountCommandValidator()
        {
            RuleFor(x => x.Firstname)
                .NotEmpty().WithMessage("Le prénom est requis.");

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage("Le nom est requis.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Le nom d'utilisateur est requis.")
                .MinimumLength(3).WithMessage("Le nom d'utilisateur doit faire au moins 3 caractères.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("L'email est requis.")
                .EmailAddress().WithMessage("Le format de l'adresse email est invalide.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Le mot de passe est requis.")
                .MinimumLength(8).WithMessage("Le mot de passe doit contenir au moins 8 caractères.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Le rôle est requis.")
                .IsEnumName(typeof(Role), caseSensitive: false)
                .WithMessage("Le rôle spécifié n'est pas un rôle valide.");
        }
    }
}
