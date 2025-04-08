using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.API.Controllers
{
    public class InstrutorController : Controller
    {
        private readonly IProfessorService _professorService;

        public InstrutorController(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstrutorViewModel>>> Index()
        {
            var instrutores = await _professorService.GetProfessores();
            return View(instrutores);
        }

        [HttpGet]
        public async Task<ActionResult<InstrutorViewModel>> CreateInstrutor()
        {
            ViewBag.Id = new SelectList(await _professorService.GetProfessores());
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<InstrutorViewModel>> CreateInstrutor(InstrutorViewModel instrutor)
        {
            var instrutorCriado = await _professorService.CreateProfessor(instrutor);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult<InstrutorViewModel>> UpdateInstrutor(int id)
        {
            var instrutores = await _professorService.GetProfessorById(id);
            return View(instrutores);
        }

        [HttpPost]
        public async Task<ActionResult<InstrutorViewModel>> UpdateInstrutor(int id, InstrutorViewModel instrutor)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(instrutor);
            }

            var instrutorAtualizado = await _professorService.UpdateProfessor(id, instrutor);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult<InstrutorViewModel>> DeleteInstrutor(int id)
        {
            var instrutor = await _professorService.GetProfessorById(id);
            return View(instrutor);
        }

        [HttpPost]
        public async Task<ActionResult<InstrutorViewModel>> DeleteInstrutor(int id, InstrutorViewModel instrutor)
        {
            if (!ModelState.IsValid)
            {
                return View(instrutor);
            }
            var instrutorDeletado = await _professorService.DeleteProfessor(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
