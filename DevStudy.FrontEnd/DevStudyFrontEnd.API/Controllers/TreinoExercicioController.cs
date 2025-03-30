using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.API.Controllers;

public class TreinoExercicioController : Controller
{
    private readonly ITreinoExercicioService _treinoExercicioService;

    public TreinoExercicioController(ITreinoExercicioService treinoExercicioService)
    {
        _treinoExercicioService = treinoExercicioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TreinoExercicioViewModel>>> Index()
    {
        var treinoExercicios = await _treinoExercicioService.GetTreinoExercicios();
        return View(treinoExercicios);
    }

    [HttpGet]
    public async Task<ActionResult> CreateTreinoExercicio()
    {
        ViewBag.Id = new SelectList(await _treinoExercicioService.GetTreinoExercicios());

        return View();
    }

    [HttpPost]
    public async Task<ActionResult> CreateTreinoExercicio(TreinoExercicioViewModel treinoExercicio)
    {
        if (!ModelState.IsValid)
        {
            return View(treinoExercicio);
        }

        var treinoExercicioCriado = await _treinoExercicioService.CreateTreinoExercicio(treinoExercicio);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<ActionResult> UpdateTreinoExercicio(int id)
    {
        var treinoExercicio = await _treinoExercicioService.GetTreinoExercicioById(id);
        return View(treinoExercicio);
    }

    [HttpPost]
    public async Task<ActionResult> UpdateTreinoExercicio(int id, TreinoExercicioViewModel treinoExercicio)
    {
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(treinoExercicio);
        }

        var treinoExercicioUpdate = await _treinoExercicioService.UpdateTreinoExercicio(id, treinoExercicio);
        if (treinoExercicioUpdate == null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<ActionResult> DeleteTreinoExercicio(int id)
    {
        var treinoExercicio = await _treinoExercicioService.GetTreinoExercicioById(id);
        return View(treinoExercicio);
    }

    [HttpPost]
    public async Task<ActionResult> DeleteTreinoExercicio(int id, TreinoExercicioViewModel treinoExercicio)
    {
        var treinoExercicioDelete = await _treinoExercicioService.DeleteTreinoExercicio(id);
        if (!treinoExercicioDelete)
        {
            return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }
}
