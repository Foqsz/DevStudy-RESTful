using DevStudy.Application.DTOs.AvaliacaoFisica;
using DevStudy.Application.Interfaces;
using DevStudy.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

    /// <summary>
    /// Get all physical evaluations.
    /// </summary>
    /// <returns>List of physical evaluations.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Get all physical evaluations", Description = "Returns a list of all physical evaluations.")]
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

    /// <summary>
    /// Get a physical evaluation by ID.
    /// </summary>
    /// <param name="id">Evaluation ID.</param>
    /// <returns>Physical evaluation.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Get a physical evaluation by ID", Description = "Returns a physical evaluation by its ID.")]
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

    /// <summary>
    /// Create a new physical evaluation.
    /// </summary>
    /// <param name="avaliacaoFisicaDTO">Evaluation data transfer object.</param>
    /// <returns>Created physical evaluation.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Create a new physical evaluation", Description = "Creates a new physical evaluation.")]
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

    /// <summary>
    /// Update an existing physical evaluation.
    /// </summary>
    /// <param name="id">Evaluation ID.</param>
    /// <param name="avaliacaoFisicaDTO">Evaluation data transfer object.</param>
    /// <returns>Updated physical evaluation.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Update an existing physical evaluation", Description = "Updates an existing physical evaluation.")]
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

    /// <summary>
    /// Delete a physical evaluation by ID.
    /// </summary>
    /// <param name="id">Evaluation ID.</param>
    /// <returns>Deleted physical evaluation.</returns>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Delete a physical evaluation by ID", Description = "Deletes a physical evaluation by its ID.")]
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
