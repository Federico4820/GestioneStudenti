using System.ComponentModel.DataAnnotations;

namespace GestioneStudenti.ViewModels
{
    public class StudenteViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public string DataDiNascita { get; set; }
        public string Email { get; set; }
    }
}
