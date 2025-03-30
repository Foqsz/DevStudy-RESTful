using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.API.Controllers;

public class TreinosController : Controller
{
    private readonly ITreinosService _treinosService;

    public TreinosController(ITreinosService treinosService)
    {
        _treinosService = treinosService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TreinoViewModel>>> Index()
    {
        var treinos = await _treinosService.GetTreinos();
        return View(treinos);
    }

    [HttpGet]
    public async Task<ActionResult> CreateTreino()
    {
        ViewBag.Id = new SelectList(await _treinosService.GetTreinos());
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> CreateTreino(TreinoViewModel treino)
    {
        if (!ModelState.IsValid)
        {
            return View(treino);
        }

        var treinoCriado = await _treinosService.CreateTreino(treino);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<ActionResult> UpdateTreino(int id)
    {
        var treino = await _treinosService.GetTreinoById(id);
        return View(treino);
    }

    [HttpPost]
    public async Task<ActionResult> UpdateTreino(int id, TreinoViewModel treino)
    {
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(treino);
        }

        var treinoUpdate = await _treinosService.UpdateTreino(id, treino);
        if (treinoUpdate == null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<ActionResult> DeleteTreino(int id)
    {
        var treino = await _treinosService.GetTreinoById(id);
        return View(treino);
    }

    [HttpPost]
    public async Task<ActionResult> DeleteTreino(int id, TreinoViewModel treino)
    {
        var treinoDelete = await _treinosService.DeleteTreino(id);
        if (!treinoDelete)
        {
            return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }
}
