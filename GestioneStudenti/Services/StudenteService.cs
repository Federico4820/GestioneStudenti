using GestioneStudenti.Data;
using GestioneStudenti.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace GestioneStudenti.Services
{
    public class StudenteService : IStudenteService
    {
        private readonly StudenteDbContext _context;

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
        }
    }
}
