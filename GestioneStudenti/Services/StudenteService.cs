using GestioneStudenti.Data;
using GestioneStudenti.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace GestioneStudenti.Services
{
    public class StudenteService : IStudenteService
    {
        private readonly StudenteDbContext _context;
        private readonly ILogger<StudenteService> _logger;

        public StudenteService(StudenteDbContext context)
        {
            _context = context;
        }

        public async Task<List<Studente>> GetAllAsync()
        {
            return await _context.Studenti.ToListAsync();
        }

        public async Task AddAsync(Studente studente)
        {
            studente.Id = Guid.NewGuid();
            _context.Add(studente);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Studente aggiunto: {studente.Nome} {studente.Cognome}");
        }

        public async Task<Studente> GetByIdAsync(Guid id)
        {
            return await _context.Studenti.FindAsync(id);
        }

        public async Task UpdateAsync(Studente studente)
        {
            _context.Update(studente);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Studente aggiornato: {studente.Nome} {studente.Cognome}");
        }

        public async Task DeleteAsync(Guid id)
        {
            var studente = await _context.Studenti.FindAsync(id);
            if (studente != null)
            {
                _context.Studenti.Remove(studente);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Studente eliminato: {studente.Nome} {studente.Cognome}");
            }
        }

    }
}
