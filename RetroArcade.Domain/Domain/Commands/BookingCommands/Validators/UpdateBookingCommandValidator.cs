using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.BookingCommands.Validators
{
    public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
    {
        public UpdateBookingCommandValidator()
        {
            RuleFor(x => x.BookingId)
                    .NotEmpty().WithMessage("L'identifiant de réservation est obligatoire.");

            RuleFor(x => x.GroupSize)
                    .InclusiveBetween(4, 10)
                    .NotEmpty().WithMessage("La taille du groupe est requis et comprise entre 4 et 10.");

            RuleFor(x => x.AccountId)
                    .NotEmpty().WithMessage("L'identifiant du compte est obligatoire.");

            RuleFor(x => x.Role)
                    .NotEmpty().WithMessage("Le role est obligatoire.");
        }
    }
}
