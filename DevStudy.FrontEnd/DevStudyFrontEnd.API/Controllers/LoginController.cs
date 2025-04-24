using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.API.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAlunoService _alunoService;
        private readonly IProfessorService _professorService;

        public LoginController(IAlunoService alunoService, IProfessorService professorService)
        {
            _alunoService = alunoService;
            _professorService = professorService;
        }

        public IActionResult LoginInstrutor()
        {
            return View();
        }

        public IActionResult LoginAluno()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginInstrutor(int id)
        {
            var instrutor = await _professorService.GetProfessorById(id);
            if (instrutor == null)
            {
                ViewBag.Erro = "Professor ID não localizado.";
                return View();
            }

            // Simula login
            TempData["InstrutorId"] = instrutor.Id;
            return RedirectToAction("Index", "Professor");
        }

        [HttpPost]
        public async Task<IActionResult> LoginAluno(int id)
        {
            var aluno = await _alunoService.GetAluno(id);
            if (aluno == null)
            {
                ViewBag.Erro = "Aluno ID não localizado.";
                return View();
            } 

            TempData["AlunoId"] = aluno.Id;
            return RedirectToAction("ImprimirTreino", "Aluno", new { id = aluno.Id });
        } 

        [HttpPost]
        public async Task<IActionResult> EncerrarLogin()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
