using Microsoft.AspNetCore.Identity;

namespace GestioneStudenti.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> ApplicationUserRole { get; set; }
    }
}
