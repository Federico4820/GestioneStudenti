using GestioneStudenti.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace GestioneStudenti.Data
{
    public class StudenteDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
        IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        // Costruttore che riceve le opzioni di configurazione del database e le passa alla classe base
        public StudenteDbContext(DbContextOptions<StudenteDbContext> options) : base(options) { }

        // Definizione delle tabelle nel database tramite DbSet<>

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        // Tabella per la relazione tra utenti e ruoli
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public DbSet<Studente> Studenti { get; set; }

        // Configurazione avanzata del modello durante la creazione del database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chiama la configurazione di IdentityDbContext per garantire che tutte le impostazioni di Identity siano applicate
            base.OnModelCreating(modelBuilder);

            // Configura la relazione tra ApplicationUserRole e ApplicationUser
            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.User) // Un ApplicationUserRole ha un utente associato
                .WithMany(u => u.ApplicationUserRole) // Un utente può avere più ruoli
                .HasForeignKey(ur => ur.UserId); // Definizione della chiave esterna che collega l'utente al ruolo

            // Configura la relazione tra ApplicationUserRole e ApplicationRole
            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(u => u.ApplicationUserRole)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}
