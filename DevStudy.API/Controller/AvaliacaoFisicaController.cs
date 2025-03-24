using DevStudy.Application.DTOs.AvaliacaoFisica;
using DevStudy.Application.Interfaces;
using DevStudy.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevStudy.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class AvaliacaoFisicaController : ControllerBase
{
    private readonly IAvaliacaoFisicaService _avaliacaoFisicaService;
    private ILogger<AvaliacaoFisicaController> _logger;

    public AvaliacaoFisicaController(IAvaliacaoFisicaService avaliacaoFisicaService, ILogger<AvaliacaoFisicaController> logger)
    {
        _avaliacaoFisicaService = avaliacaoFisicaService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AvaliacaoFisica>>> GetAvaliacoes()
    {
        var avaliacaoAll = await _avaliacaoFisicaService.GetAvaliacoesFisicas();

        if (avaliacaoAll == null)
        {
            _logger.LogError("Nenhuma avaliação encontrada.");
            return NotFound("Nenhuma avaliação encontrada no sistema.");
        }

        return Ok(avaliacaoAll);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AvaliacaoFisica>> GetAvaliacao(int id)
    {
        var avaliacaoId = await _avaliacaoFisicaService.GetAvaliacaoFisica(id);
        if (avaliacaoId == null)
        {
            _logger.LogError($"Avaliação de ID={id} não encontrada.");
            return NotFound($"Avaliação de ID={id} não encontrada.");
        }
        return Ok(avaliacaoId);
    }

    [HttpPost]
    public async Task<ActionResult<AvaliacaoFisicaDTO>> CreateAvaliacao([FromBody] AvaliacaoFisicaDTO avaliacaoFisicaDTO)
    {
        var newAvaliacao = await _avaliacaoFisicaService.CreateAvaliacaoFisica(avaliacaoFisicaDTO);
        if (newAvaliacao == null)
        {
            _logger.LogError("Erro ao criar avaliação.");
            return BadRequest("Erro ao criar avaliação.");
        }
        return Ok(newAvaliacao);
    }

    [HttpPut]
    public async Task<ActionResult<AvaliacaoFisicaDTO>> UpdateAvaliacao(int id, [FromBody] AvaliacaoFisicaDTO avaliacaoFisicaDTO)
    {
        var updateAvaliacao = await _avaliacaoFisicaService.UpdateAvaliacaoFisica(id, avaliacaoFisicaDTO);

        if (updateAvaliacao == null)
        {
            _logger.LogError($"Erro ao atualizar avaliação de ID={id}.");
            return BadRequest($"Erro ao atualizar avaliação de ID={id}.");
        }

        return Ok(updateAvaliacao);
    }

    [HttpDelete]
    public async Task<ActionResult<AvaliacaoFisica>> AvaliacaoFisica(int id)
    {
        var deleteAvaliacao = await _avaliacaoFisicaService.DeleteAvaliacaoFisica(id);
        if (!deleteAvaliacao)
        {
            _logger.LogError($"Erro ao deletar avaliação de ID={id}.");
            return NotFound($"Não foi localizado avaliação com o id={id}");
        }
        return Ok(deleteAvaliacao);
    }
}
