using System.ComponentModel.DataAnnotations;

namespace GestioneStudenti.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required DateOnly BirthDate { get; set; }

        [Required]
        public required string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public required string ConfirmPassword { get; set; }
    }
}
