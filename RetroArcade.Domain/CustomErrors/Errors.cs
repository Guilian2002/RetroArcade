using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Results;

namespace RetroArcade.Domain.CustomErrors
{
    internal static class Errors
    {
        // throw an error when the account is not found
        internal static Error AccountNotFound => Error.Create("Compte non trouvée");
        // throw an error when the building is not found
        internal static Error BuildingNotFound => Error.Create("Batiment non trouvée");

        // throw an error when the room is not found
        internal static Error RoomNotFound => Error.Create("Pièce non trouvée");

    }
}
