using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.API.Controllers
{
    public class AvaliacaoFisicaController : Controller
    {
        private readonly IAvaliacaoFisicaService _avaliacaoFisicaService;

        public AvaliacaoFisicaController(IAvaliacaoFisicaService avaliacaoFisicaService)
        {
            _avaliacaoFisicaService = avaliacaoFisicaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvaliacaoFisicaViewModel>>> Index()
        {
            var avaliacoes = await _avaliacaoFisicaService.GetAvaliacoesFisicas();
            return View(avaliacoes); 
        }

        [HttpGet]
        public async Task<ActionResult> CreateAvaliacaoFisica()
        {
            ViewBag.Id = new SelectList(await _avaliacaoFisicaService.GetAvaliacoesFisicas());
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAvaliacaoFisica(AvaliacaoFisicaViewModel avaliacaoFisica)
        {
            if (!ModelState.IsValid)
            {
                return View(avaliacaoFisica);
            }
            var avaliacaoCriada = await _avaliacaoFisicaService.CreateAvaliacaoFisica(avaliacaoFisica);
            return RedirectToAction(nameof(Index));
        }
    }
}
