using RetroArcade.Domain.Domain.Commands.CategorieCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.CategorieQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Commands;
using Tools.Cqs.Queries;

namespace RetroArcade.Domain.Domain.Repositories
{
    public interface ICategorieRepository : 
        ICommandHandler<AddCategorieCommand>,
        ICommandHandler<DeleteCategorieCommand>,
        IQueryHandler<GetAllCategoriesQuery, IEnumerable<Categorie>>
    {
    }
}
