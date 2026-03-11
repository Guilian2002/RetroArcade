using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;

namespace RetroArcade.Domain.Domain.Commands.CategorieCommands
{
    public sealed class DeleteCategorieCommand : ICommandDefinition
    {
        public Guid Id { get; set; }

        public DeleteCategorieCommand(Guid id)
        {
            Id = id;
        }
    }
}
