using GestioneStudenti.Models;
using Microsoft.EntityFrameworkCore;

namespace GestioneStudenti.Data
{
    public class StudenteDbContext : DbContext
    {
        public StudenteDbContext(DbContextOptions<StudenteDbContext> options) : base(options) { }

        public DbSet<Studente> Studenti { get; set; }
    }
}
