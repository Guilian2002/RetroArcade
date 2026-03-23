using RetroArcade.BlazorWebAssembly.Models.RetroArcade.Enum;
using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.Authentification
{
    public class UpdateAccountForm
    {
        [Required]
        public Guid AccountId { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [StringLength(50, ErrorMessage = "Le prénom est trop long.")]
        [Display(Name = "Prénom")]
        public string Firstname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(50, ErrorMessage = "Le nom est trop long.")]
        [Display(Name = "Nom")]
        public string Lastname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Le pseudo doit faire entre 3 et 20 caractères.")]
        [Display(Name = "Nom d'utilisateur")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Veuillez sélectionner un rôle.")]
        [EnumDataType(typeof(Role), ErrorMessage = "Ce n'est pas un rôle valide.")]
        [Display(Name = "Rôle")]
        public string Role { get; set; } = string.Empty;
    }
}
