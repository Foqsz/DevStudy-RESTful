using DevStudy.Application.Interfaces;
using DevStudy.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevStudy.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class PlanoController : ControllerBase
{
    private readonly IPlanoService _planoService;
    private ILogger<PlanoController> _logger;

    public PlanoController(IPlanoService planoService, ILogger<PlanoController> logger)
    {
        _planoService = planoService;
        _logger = logger;
    }

    /// <summary>
    /// Obtém todos os planos.
    /// </summary>
    /// <returns>Lista de planos.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obtém todos os planos", Description = "Retorna uma lista de todos os planos cadastrados.")]
    public async Task<ActionResult<IEnumerable<Plano>>> GetPlanos()
    {
        var listarPlanos = await _planoService.GetPlanos();

        if (listarPlanos == null)
        {
            _logger.LogError("Nao foi localizado nenhum plano cadastrado");
            return NotFound("Nenhum plano registrado no sistema.");
        }

        return Ok(listarPlanos);
    }

    /// <summary>
    /// Obtém um plano pelo ID.
    /// </summary>
    /// <param name="id">ID do plano.</param>
    /// <returns>Plano correspondente ao ID.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "Obtém um plano pelo ID", Description = "Retorna um plano específico pelo ID.")]
    public async Task<ActionResult<Plano>> GetPlanoById(int id)
    {
        var listarPlanoById = await _planoService.GetPlano(id);

        if (listarPlanoById == null)
        {
            _logger.LogError($"Nao foi localizado o plano id={id}");
            return NotFound($"Nao foi localizado o plano id={id}");
        }

        return Ok(listarPlanoById);
    }

    /// <summary>
    /// Cria um novo plano.
    /// </summary>
    /// <param name="plano">Dados do novo plano.</param>
    /// <returns>Plano criado.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Cria um novo plano", Description = "Cadastra um novo plano no sistema.")]
    public async Task<ActionResult<Plano>> CreatePlano(Plano plano)
    {
        var newPlano = await _planoService.CreatePlano(plano);

        if (newPlano == null)
        {
            _logger.LogError("Nao foi possivel cadastrar o plano.");
            return BadRequest("Não foi possível cadastrar esse plano.");
        }

        return CreatedAtAction(nameof(GetPlanoById), new { id = newPlano.Id }, newPlano);
    }

    /// <summary>
    /// Atualiza um plano existente.
    /// </summary>
    /// <param name="id">ID do plano a ser atualizado.</param>
    /// <param name="plano">Dados atualizados do plano.</param>
    /// <returns>Plano atualizado.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Atualiza um plano existente", Description = "Atualiza os dados de um plano existente.")]
    public async Task<ActionResult<Plano>> UpdatePlano(int id, Plano plano)
    {
        if (id != plano.Id)
        {
            _logger.LogError($"Id informado {id} diferente do id do plano {plano.Id}");
            return BadRequest($"Id informado {id} diferente do id do plano {plano.Id}");
        }

        var updatePlano = await _planoService.UpdatePlano(id, plano);
        if (updatePlano == null)
        {
            _logger.LogError($"Nao foi possivel atualizar o plano id={id}");
            return BadRequest($"Nao foi possivel atualizar o plano id={id}");
        }
        return Ok(updatePlano);
    }

    /// <summary>
    /// Deleta um plano pelo ID.
    /// </summary>
    /// <param name="id">ID do plano a ser deletado.</param>
    /// <returns>Confirmação da exclusão.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Deleta um plano pelo ID", Description = "Remove um plano específico pelo ID.")]
    public async Task<ActionResult<bool>> DeletePlano(int id)
    {
        var deletePlano = await _planoService.DeletePlano(id);
        if (!deletePlano)
        {
            _logger.LogError($"Nao foi possivel deletar o plano id={id}");
            return BadRequest($"Nao foi possivel deletar o plano id={id}");
        }
        return Ok(deletePlano);
    }
}
