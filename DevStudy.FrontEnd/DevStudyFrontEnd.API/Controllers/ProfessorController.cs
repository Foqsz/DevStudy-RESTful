using Microsoft.AspNetCore.Mvc;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.API.Controllers
{
    public class ProfessorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EncerrarLogin()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
