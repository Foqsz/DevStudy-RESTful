﻿using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    // GET: exibe o formulário
    [HttpGet]
    public IActionResult ImprimirTreino()
    {
        return View();
    }

    // POST: redireciona para a API que gera o PDF
    [HttpPost]
    public async Task<IActionResult> ImprimirTreino(int id)
    {
        if (id <= 0)
        {
            TempData["Erro"] = "ID inválido. Tente novamente.";
            return RedirectToAction("ImprimirTreino");
        }

        var url = $"https://localhost:7238/api/Aluno/treinos/imprimirTreino?idAluno={id}";  
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)  
            {
                TempData["Erro"] = $"Não foi localizado nenhum treino ou aluno com o ID={id} informado.";
                return RedirectToAction("ImprimirTreino");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            if (responseContent.Contains("Aluno ou treinos não encontrados."))
            {
                TempData["Erro"] = "Aluno ou treinos não encontrados. Tente novamente.";
                return RedirectToAction("ImprimirTreino");
            }
        }

        return Redirect(url);
    }

    [HttpPost]
    public async Task<IActionResult> EncerrarLogin()
    {  
        // Redireciona para a página inicial
        return RedirectToAction("Index", "Home");
    }


    [HttpGet]  
    public async Task<ActionResult> CreateAluno()
    {
        ViewBag.Id = new SelectList(await _alunoService.GetAlunos());

        return View();
    }

    [HttpPost]
    public async Task<ActionResult> CreateAluno(AlunoViewModel aluno)
    {
        //if (!ModelState.IsValid)
        //{
        //    return View(aluno);
        //}

        var alunoCriado = await _alunoService.AddAluno(aluno);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<ActionResult> UpdateAluno(int id)
    {
        var aluno = await _alunoService.GetAluno(id);
        Console.WriteLine($"PlanoId: {aluno.PlanoId}, InstrutorId: {aluno.InstrutorId}, {aluno.Email}");
        return View(aluno);
    }

    [HttpPost]
    public async Task<ActionResult> UpdateAluno(int id, AlunoViewModel aluno)
    {
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(aluno);
        } 

        var alunoUpdate = await _alunoService.UpdateAluno(id, aluno);
        if (alunoUpdate == null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<ActionResult> DeleteAluno(int id)
    {
        var aluno = await _alunoService.GetAluno(id);
        return View(aluno);
    }

    [HttpPost]
    public async Task<ActionResult> DeleteAluno(int id, AlunoViewModel aluno)
    {
        var alunoDelete = await _alunoService.DeleteAluno(id);
        if (!alunoDelete)
        {
            return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

}
