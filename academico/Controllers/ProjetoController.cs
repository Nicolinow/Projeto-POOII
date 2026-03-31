using Microsoft.AspNetCore.Mvc;
using academico.Models;
using academico.Repositories;

namespace academico.Controllers
{
    public class ProjetoController : Controller
    {
        private readonly IProjetoRepository _projetoRepository;

        public ProjetoController(IProjetoRepository repository)
        {
            _projetoRepository = repository;
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var projetos = await _projetoRepository.GetAll(ct);
            return View(projetos);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Projeto projeto, CancellationToken ct)
        {
            if (ModelState.IsValid)
            {
                await _projetoRepository.Create(projeto, ct);
                return RedirectToAction(nameof(Index));
            }
            return View(projeto);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var projeto = await _projetoRepository.GetId(id, ct);
            if (projeto == null) return NotFound();

            return View(projeto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Projeto projeto, CancellationToken ct)
        {
            if (id != projeto.ProjetoId) return NotFound();

            if (ModelState.IsValid)
            {
                await _projetoRepository.Edit(projeto, ct);
                return RedirectToAction(nameof(Index));
            }
            return View(projeto);
        }

        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var projeto = await _projetoRepository.GetId(id, ct);
            if (projeto == null) return NotFound();

            return View(projeto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
        {
            await _projetoRepository.Delete(id, ct);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var projeto = await _projetoRepository.GetId(id);
            if (projeto == null)
                return NotFound();

            return View(projeto);
        }
    }
}