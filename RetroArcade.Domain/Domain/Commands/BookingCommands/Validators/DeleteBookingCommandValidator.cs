using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.BookingCommands.Validators
{
    public class DeleteBookingCommandValidator : AbstractValidator<DeleteBookingCommand>
    {
        public DeleteBookingCommandValidator()
        {
            RuleFor(x => x.BookingId)
                    .NotEmpty().WithMessage("L'identifiant de réservation est obligatoire.");
            RuleFor(x => x.AccountId)
                    .NotEmpty().WithMessage("L'identifiant du compte est obligatoire.");
            RuleFor(x => x.Role)
                    .NotEmpty().WithMessage("Le role est obligatoire.");
        }
    }
}
