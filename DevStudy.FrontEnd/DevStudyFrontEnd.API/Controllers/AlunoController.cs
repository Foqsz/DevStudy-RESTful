using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.API.Controllers;

public class AlunoController : Controller
{
    private readonly IAlunoService _alunoService;
    
    public AlunoController(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlunoViewModel>>> Index()
    {
        var alunos = await _alunoService.GetAlunos();
        return View(alunos);
    }
}
