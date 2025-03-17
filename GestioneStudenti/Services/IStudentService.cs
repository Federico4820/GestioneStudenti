using GestioneStudenti.Models;

namespace GestioneStudenti.Services
{
    public interface IStudenteService
    {
        Task<List<Studente>> GetAllAsync();
        Task AddAsync(Studente studente);
    }
}
