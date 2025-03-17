using System.ComponentModel.DataAnnotations;

namespace GestioneStudenti.Models
{
    public class Studente
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }

        [Required]
        public DateTime DataDiNascita { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }
    }
}
