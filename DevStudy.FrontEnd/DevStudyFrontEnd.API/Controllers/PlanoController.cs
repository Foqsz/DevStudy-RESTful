using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.API.Controllers;

public class PlanoController : Controller
{
    private readonly IPlanoService _planoService;

    public PlanoController(IPlanoService planoService)
    {
        _planoService = planoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlanoViewModel>>> Index()
    {
        var planos = await _planoService.GetPlanos();
        return View(planos);
    }

    [HttpGet]
    public async Task<ActionResult<PlanoViewModel>> CreatePlano()
    {
        ViewBag.Id = new SelectList(await _planoService.GetPlanos());
 
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<PlanoViewModel>> CreatePlano(PlanoViewModel plano)
    {
        if (ModelState.IsValid)
        {
            await _planoService.CreatePlano(plano);
            return RedirectToAction(nameof(Index));
        }
        return View(plano);
    }

    [HttpGet]
    public async Task<ActionResult<PlanoViewModel>> UpdatePlano(int id)
    {
        var plano = await _planoService.GetPlano(id);
        return View(plano);
    }

    [HttpPost]
    public async Task<ActionResult<PlanoViewModel>> UpdatePlano(int id, PlanoViewModel plano)
    {
        if (ModelState.IsValid)
        {
            await _planoService.UpdatePlano(id, plano);
            return RedirectToAction(nameof(Index));
        }
        return View(plano);
    }

    [HttpGet]
    public async Task<ActionResult<PlanoViewModel>> DeletePlano(int id)
    {
        var plano = await _planoService.GetPlano(id);
        return View(plano);
    }

    [HttpPost]
    public async Task<ActionResult<PlanoViewModel>> DeletePlano(int id, PlanoViewModel plano)
    {
        await _planoService.DeletePlano(id);
        return RedirectToAction(nameof(Index));
    }   
}
