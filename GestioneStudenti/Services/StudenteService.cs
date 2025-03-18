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

        public StudenteService(StudenteDbContext context, ILogger<StudenteService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Studente>> GetAllAsync()
        {
            _logger.LogInformation("Tentativo di recuperare tutti gli studenti.");
            try
            {
                var studenti = await _context.Studenti.ToListAsync();
                _logger.LogInformation($"Recuperati {studenti.Count} studenti.");
                return studenti;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero degli studenti.");
                throw;
            }
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
            _logger.LogInformation($"Tentativo di recuperare lo studente con ID: {id}");
            try
            {
                var studente = await _context.Studenti.FindAsync(id);
                if (studente == null)
                {
                    _logger.LogWarning($"Studente con ID {id} non trovato.");
                }
                return studente;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante il recupero dello studente con ID: {id}");
                throw;
            }
        }

        public async Task UpdateAsync(Studente studente)
        {
            try
            {
                studente.Id = Guid.NewGuid();
                _context.Add(studente);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Studente aggiunto: {studente.Nome} {studente.Cognome}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante l'aggiunta dello studente: {studente.Nome} {studente.Cognome}");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var studente = await _context.Studenti.FindAsync(id);
                if (studente != null)
                {
                    _context.Studenti.Remove(studente);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Studente eliminato: {studente.Nome} {studente.Cognome}");
                }
                else
                {
                    _logger.LogWarning($"Tentativo di eliminare uno studente non trovato con ID: {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore durante l'eliminazione dello studente con ID: {id}");
                throw;
            }
        }

    }
}
