using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RetroArcade.ASPCore.Models.Authentification
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Prénom : ")]
        [Required(ErrorMessage = "Le prénom est obligatoire!")]
        [StringLength(32, ErrorMessage = "Le prénom doit contenir moins de 32 caractères.")]
        public string Firstname { get; set; } = string.Empty;

        [DisplayName("Nom : ")]
        [Required(ErrorMessage = "Le nom est obligatoire!")]
        [StringLength(32, ErrorMessage = "Le nom doit contenir moins de 32 caractères.")]
        public string Lastname { get; set; } = string.Empty;

        [DisplayName("Nom d\'utilisateur : ")]
        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire!")]
        [StringLength(32, ErrorMessage = "Le nom d'utilisateur doit contenir moins de 32 caractères.")]
        public string Username { get; set; } = string.Empty;

        [DisplayName("Email : ")]
        [Required(ErrorMessage = "L'adresse email est obligatoire!")]
        [EmailAddress(ErrorMessage = "Format d'email invalide.")]
        [StringLength(320, ErrorMessage = "L'email est trop long.")]
        public string Email { get; set; } = string.Empty;

        [DisplayName("Mot de passe : ")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Le mot de passe est obligatoire!")]
        [MinLength(8, ErrorMessage = "Le mot de passe doit faire au moins 8 caractères.")]
        [StringLength(64, ErrorMessage = "Le mot de passe est trop long.")]
        public string Password { get; set; } = string.Empty;

        [DisplayName("Confirmer le mot de passe : ")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La confirmation est obligatoire !")]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
