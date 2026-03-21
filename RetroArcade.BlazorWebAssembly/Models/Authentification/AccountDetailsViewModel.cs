using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.Authentification
{
    public class AccountDetailsViewModel : AccountViewModel
    {
        [DisplayName("Rôle")]
        public Role Role { get; set; }

        [DisplayName("Date de création")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? CreationDate { get; set; }

        [DisplayName("Date de désactivation")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? DisableDate { get; set; }

        [DisplayName("État du compte")]
        public bool IsActive { get; set; }

        public string StatusLabel => IsActive ? "Actif" : "Désactivé";
        public string StatusClass => IsActive ? "bg-success" : "bg-danger";
    }
}
