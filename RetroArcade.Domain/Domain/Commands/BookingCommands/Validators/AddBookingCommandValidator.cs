using FluentValidation;
using RetroArcade.Domain.Domain.Commands.BookingCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.BookingCommands.Validators
{
    public class AddBookingCommandValidator : AbstractValidator<AddBookingCommand>
    {
        public AddBookingCommandValidator()
        {
            RuleFor(x => x.BeginDate)
                .NotEmpty().WithMessage("La date de début est obligatoire.")
                .Must(date => date > DateTime.MinValue).WithMessage("La date de début est invalide.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("La date de fin est obligatoire.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Le statut est requis.");

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("L'identifiant de la salle est requis.");

            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("L'identifiant du compte est requis.");

            RuleFor(x => x.GroupSize)
                .InclusiveBetween(4, 10)
                .WithMessage("La taille du groupe doit être comprise entre 4 et 10 personnes.");

            RuleFor(x => x.BeginDate)
                .LessThan(x => x.EndDate)
                .WithMessage("L'heure de début doit être strictement antérieure à l'heure de fin.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Le prix ne peut pas être négatif.");
        }
    }
}
