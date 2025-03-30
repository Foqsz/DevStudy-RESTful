using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.API.Controllers
{
    public class ExerciciosController : Controller
    {
        private readonly IExercicioService _exercicioService;

        public ExerciciosController(IExercicioService exercicioService)
        {
            _exercicioService = exercicioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExercicioViewModel>>> Index()
        {
            var exercicios = await _exercicioService.GetExercicios();
            return View(exercicios);
        }

        [HttpGet]
        public async Task<ActionResult> CreateExercicio()
        {
            ViewBag.Id = new SelectList(await _exercicioService.GetExercicios());

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateExercicio(ExercicioViewModel exercicio)
        {
            if (!ModelState.IsValid)
            {
                return View(exercicio);
            }

            var exercicioCriado = await _exercicioService.CreateExercicio(exercicio);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> UpdateExercicio(int id)
        {
            var exercicio = await _exercicioService.GetExercicioById(id);
            return View(exercicio);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateExercicio(int id, ExercicioViewModel exercicio)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(exercicio);
            }

            var exercicioUpdate = await _exercicioService.UpdateExercicio(id, exercicio);
            if (exercicioUpdate == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> DeleteExercicio(int id)
        {
            var exercicio = await _exercicioService.GetExercicioById(id);
            return View(exercicio);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteExercicio(int id, ExercicioViewModel exercicio)
        {
            var exercicioDelete = await _exercicioService.DeleteExercicio(id);
            if (!exercicioDelete)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
