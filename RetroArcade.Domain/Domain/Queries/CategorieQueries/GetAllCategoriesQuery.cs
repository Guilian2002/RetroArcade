using RetroArcade.Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Queries;

namespace RetroArcade.Domain.Domain.Queries.CategorieQueries
{
    public sealed class GetAllCategoriesQuery : IQueryDefinition<IEnumerable<Categorie>>
    {
    }
}
