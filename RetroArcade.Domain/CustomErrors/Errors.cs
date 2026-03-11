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
        internal static Error AccountNotFound => Error.Create("Compte non trouvé");
        // throw an error when the building is not found
        internal static Error BuildingNotFound => Error.Create("Batiment non trouvé");
        // throw an error when the room is not found
        internal static Error RoomNotFound => Error.Create("Pièce non trouvée");
        // throw an error when the booking is not found
        internal static Error BookingNotFound => Error.Create("Réservation non trouvée");
        // throw an error when the arcade machine is not found
        internal static Error ArcadeMachineNotFound => Error.Create("Catalogue de la machine d'arcade non trouvé");
        // throw an error when the arcade machine is not found
        internal static Error CategorieNotFound => Error.Create("Categorie de la machine d'arcade non trouvée");
        // throw an error when the room's arcade machine is not found
        internal static Error RoomArcadeMachineNotFound => Error.Create("Machine d'arcade non trouvée");
        // throw an error when the room feedback is not found
        internal static Error RoomFeedbackNotFound => Error.Create("Commentaire non trouvée");
    }
}
