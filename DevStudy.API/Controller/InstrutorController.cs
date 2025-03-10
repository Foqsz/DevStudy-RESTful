using DevStudy.Application.DTOs.Instrutor;
using DevStudy.Application.Interfaces;
using DevStudy.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevStudy.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class InstrutorController : ControllerBase
{
    private readonly IInstrutorService _instrutorService;
    private ILogger<InstrutorController> _logger;

    public InstrutorController(IInstrutorService instrutorService, ILogger<InstrutorController> logger)
    {
        _instrutorService = instrutorService;
        _logger = logger;
    }

    /// <summary>
    /// Obtém todos os instrutores.
    /// </summary>
    /// <returns>Lista de instrutores.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Obtém todos os instrutores", Description = "Retorna uma lista de todos os instrutores.")]
    public async Task<ActionResult<IEnumerable<Instrutor>>> GetInstrutores()
    {
        try
        {
            var instrutores = await _instrutorService.GetInstrutores();

            if (instrutores == null)
            {
                _logger.LogError("Nenhum instrutor localizado");
                return NotFound("Instrutores não localizados.");
            }

            return Ok(instrutores);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Obtém um instrutor pelo ID.
    /// </summary>
    /// <param name="id">ID do instrutor.</param>
    /// <returns>Instrutor.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Obtém um instrutor pelo ID", Description = "Retorna um instrutor específico pelo ID.")]
    public async Task<ActionResult<Instrutor>> GetInstrutor(int id)
    {
        var instrutorId = await _instrutorService.GetInstrutor(id);
        if (instrutorId == null)
        {
            _logger.LogError("Instrutor não encontrado");
            return NotFound($"Instrutor id={id} não localizado.");
        }
        return Ok(instrutorId);
    }

    /// <summary>
    /// Obtém um instrutor pelo e-mail.
    /// </summary>
    /// <param name="email">E-mail do instrutor.</param>
    /// <returns>Instrutor.</returns>
    [HttpGet("{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Obtém um instrutor pelo e-mail", Description = "Retorna um instrutor específico pelo e-mail.")]
    public async Task<ActionResult<Instrutor>> GetInstrutorByEmail(string email)
    {
        var instrutorEmail = await _instrutorService.GetInstrutorByEmail(email);
        if (instrutorEmail == null)
        {
            _logger.LogError("Instrutor não encontrado");
            return NotFound($"Instrutor com e-mail({email}) não localizado.");
        }
        return Ok(instrutorEmail);
    }

    /// <summary>
    /// Cria um novo instrutor.
    /// </summary>
    /// <param name="instrutor">Dados do instrutor.</param>
    /// <returns>Instrutor criado.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Cria um novo instrutor", Description = "Cria um novo instrutor com os dados fornecidos.")]
    public async Task<ActionResult<InstrutorDTO>> CreateInstrutor([FromBody] InstrutorDTO instrutor)
    {
        try
        {
            var newInstrutor = await _instrutorService.CreateInstrutor(instrutor);

            if (newInstrutor == null)
            {
                _logger.LogError("Instrutor não cadastrado");
                return BadRequest("Instrutor não cadastrado.");
            }

            return CreatedAtAction(nameof(GetInstrutor), new { id = newInstrutor.Id }, newInstrutor);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Atualiza um instrutor existente.
    /// </summary>
    /// <param name="id">ID do instrutor.</param>
    /// <param name="instrutor">Dados do instrutor.</param>
    /// <returns>Instrutor atualizado.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Atualiza um instrutor existente", Description = "Atualiza os dados de um instrutor existente.")]
    public async Task<ActionResult<InstrutorDTO>> UpdateInstrutor(int id, [FromBody] InstrutorDTO instrutor)
    {
        try
        {
            var updateInstrutor = await _instrutorService.UpdateInstrutor(id, instrutor);

            if (updateInstrutor == null)
            {
                _logger.LogError("Instrutor não atualizado");
                return BadRequest("Instrutor não atualizado.");
            }
            return Ok(updateInstrutor);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// Deleta um instrutor pelo ID.
    /// </summary>
    /// <param name="id">ID do instrutor.</param>
    /// <returns>Resultado da operação.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Deleta um instrutor pelo ID", Description = "Deleta um instrutor específico pelo ID.")]
    public async Task<ActionResult<bool>> DeleteInstrutor(int id)
    {
        try
        {
            var deleteInstrutor = await _instrutorService.DeleteInstrutor(id);

            if (!deleteInstrutor)
            {
                _logger.LogError("Instrutor não deletado");
                return BadRequest($"Instrutor id={id} não deletado, não foi localizado.");
            }

            return Ok(deleteInstrutor);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
