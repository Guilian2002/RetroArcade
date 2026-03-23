using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.RoomCommands.Validators
{
    public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomCommandValidator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("L'identifiant de la pièce est obligatoire.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Le nom est obligatoire.");

            RuleFor(x => x.Number)
                .InclusiveBetween(1, 25).WithMessage("Le numéro de salle doit être compris entre 1 et 25.");

            RuleFor(x => x.MachineCapacity)
                .Equal(10).WithMessage("La capacité machine doit être exactement de 10.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Le prix ne peut pas être négatif.");

            RuleFor(x => x.BuildingId)
                .NotEmpty().WithMessage("L'identifiant du bâtiment est obligatoire.");
        }
    }
}
