using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.BuildingCommands.Validators
{
    public class UpdateBuildingCommandValidator : AbstractValidator<UpdateBuildingCommand>
    {
        public UpdateBuildingCommandValidator()
        {
            RuleFor(x => x.BuildingId)
                .NotEmpty().WithMessage("L'identifiant du batiment est obligatoire.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Le nom est obligatoire.");

            RuleFor(x => x.OpeningHour)
                .NotNull().WithMessage("L'heure d'ouverture est obligatoire.");

            RuleFor(x => x.ClosingHour)
                .NotNull().WithMessage("L'heure de fermeture est obligatoire.")
                .GreaterThan(x => x.OpeningHour).WithMessage("L'heure d'ouverture doit être antérieure à l'heure de fermeture.");

            RuleFor(x => x.AddressStreet)
                .NotEmpty().WithMessage("La rue est obligatoire.");

            RuleFor(x => x.AddressNumber)
                .NotEmpty().WithMessage("Le numéro de rue est obligatoire.");

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("Le code postal est obligatoire.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("La ville est obligatoire.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Le pays est obligatoire.");

            RuleFor(x => x.ManagerId)
                .NotEmpty().WithMessage("L'identifiant du manager est obligatoire.");
        }
    }
}
