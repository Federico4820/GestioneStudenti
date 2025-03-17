using GestioneStudenti.Models;
using GestioneStudenti.Services;
using GestioneStudenti.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestioneStudenti.Controllers
{
    public class StudentiController : Controller
    {
        private readonly IStudenteService _studenteService;

        public StudentiController(IStudenteService studenteService)
        {
            _studenteService = studenteService;
        }

        public async Task<IActionResult> Index()
        {
            var studenti = await _studenteService.GetAllAsync();
            var viewModel = studenti.Select(s => new StudenteViewModel
            {
                Id = s.Id,
                NomeCompleto = $"{s.Nome} {s.Cognome}",
                DataDiNascita = s.DataDiNascita.ToString("dd/MM/yyyy"),
                Email = s.Email
            });
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Studente studente)
        {
            if (ModelState.IsValid)
            {
                await _studenteService.AddAsync(studente);
                var studenti = await _studenteService.GetAllAsync();
                var viewModel = studenti.Select(s => new StudenteViewModel
                {
                    Id = s.Id,
                    NomeCompleto = $"{s.Nome} {s.Cognome}",
                    DataDiNascita = s.DataDiNascita.ToString("dd/MM/yyyy"),
                    Email = s.Email
                });
                return PartialView("_StudentiList", viewModel);
            }
            return PartialView("_Create", studente);
        }
    }
}
