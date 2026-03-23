using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroArcade.Domain.Domain.Commands.BuildingCommands.Validators
{
    public class DeleteBuildingCommandValidator : AbstractValidator<DeleteBuildingCommand>
    {
        public DeleteBuildingCommandValidator()
        {
            RuleFor(x => x.BuildingId)
                .NotEmpty().WithMessage("L'identifiant du batiment est obligatoire.");
        }
    }
}
