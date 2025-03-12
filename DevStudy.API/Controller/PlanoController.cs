using DevStudy.Application.Interfaces;
using DevStudy.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
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

    [HttpGet("{id:int}")]
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

    [HttpPost]
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

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Plano>> UpdatePlano(int id, Plano plano)
    {
        if(id != plano.Id)
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

    [HttpDelete("{id:int}")]
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
