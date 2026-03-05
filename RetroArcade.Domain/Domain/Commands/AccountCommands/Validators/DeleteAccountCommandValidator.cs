using FluentValidation;
using RetroArcade.Domain.Domain.Commands.BookingCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.AccountCommands.Validators
{
    public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
    {
        public DeleteAccountCommandValidator()
        {
            RuleFor(x => x.AccountId)
                    .NotEmpty().WithMessage("L'identifiant du compte est obligatoire.");
        }
    }
}
