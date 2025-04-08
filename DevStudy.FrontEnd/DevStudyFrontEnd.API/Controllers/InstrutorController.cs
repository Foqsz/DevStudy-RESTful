using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
