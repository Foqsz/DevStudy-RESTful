using DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;
using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.API.Controllers;

public class PagamentoController : Controller
{ 
    private readonly IPagamentoService _pagamentoService;

    public PagamentoController(IPagamentoService pagamentoService)
    {
        _pagamentoService = pagamentoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PagamentoViewModel>>> Index()
    {
        var pagamentos = await _pagamentoService.GetPagamentos();
        return View(pagamentos);
    }

    [HttpGet]
    public async Task<ActionResult<PagamentoViewModel>> CreatePagamento()
    {
        ViewBag.Id = new SelectList(await _pagamentoService.GetPagamentos());

        return View();
    }

    [HttpPost]
    public async Task<ActionResult<PagamentoViewModel>> CreatePagamento(PagamentoViewModel pagamento)
    {
        if (ModelState.IsValid)
        {
            await _pagamentoService.CreatePagamento(pagamento);
            return RedirectToAction(nameof(Index));
        }
        return View(pagamento);
    }

    [HttpGet]
    public async Task<ActionResult<PagamentoViewModel>> UpdatePagamento(int id)
    {
        var pagamento = await _pagamentoService.GetPagamento(id);
        return View(pagamento);
    }

    [HttpPost]
    public async Task<ActionResult<PagamentoViewModel>> UpdatePagamento(int id, PagamentoViewModel pagamento)
    {
        if (ModelState.IsValid)
        {
            await _pagamentoService.UpdatePagamento(id, pagamento);
            return RedirectToAction(nameof(Index));
        }
        return View(pagamento);
    }

    [HttpGet]
    public async Task<ActionResult<PagamentoViewModel>> DeletePagamento(int id)
    {
        var deletePagamento = await _pagamentoService.GetPagamento(id);

        return View(deletePagamento);
    }

    [HttpPost]
    public async Task<ActionResult<bool>> DeletePagamento(int id, PagamentoViewModel pagamento)
    {
        var result = await _pagamentoService.DeletePagamento(id);
        return RedirectToAction(nameof(Index));
    }
}
