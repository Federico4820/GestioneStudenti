using GestioneStudenti.Models;

namespace GestioneStudenti.Services
{
    public interface IStudenteService
    {
        Task<List<Studente>> GetAllAsync();
        Task AddAsync(Studente studente);
        Task<Studente> GetByIdAsync(Guid id);
        Task UpdateAsync(Studente studente);
        Task DeleteAsync(Guid id);
    }
}
