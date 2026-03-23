using System.ComponentModel.DataAnnotations;

namespace RetroArcade.BlazorWebAssembly.Models.Authentification
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "L'adresse email est requise.")]
        [EmailAddress(ErrorMessage = "Format d'email invalide.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; } = string.Empty;
    }
}
