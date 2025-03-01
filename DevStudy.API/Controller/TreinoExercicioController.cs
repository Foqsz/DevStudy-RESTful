using DevStudy.Application.DTOs.TreinoExercicio;
using DevStudy.Application.Interfaces; 
using DevStudy.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevStudy.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreinoExercicioController : ControllerBase
    {
        private readonly ITreinoExercicioService _treinoExercicioService;
        private ILogger _logger;

        public TreinoExercicioController(ITreinoExercicioService treinoExercicioService, ILogger<TreinoExercicioController> logger)
        {
            _treinoExercicioService = treinoExercicioService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreinoExercicio>>> GetTreinoExercicios()
        {
            try
            {
                var treinoExercicios = await _treinoExercicioService.GetTreinoExercicios();
                if (treinoExercicios == null)
                {
                    return NotFound();
                }
                return Ok(treinoExercicios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar treino exercicios");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TreinoExercicio>> GetTreinoExercicioById(int id)
        {
            try
            {
                var getTreinoExercicioId = await _treinoExercicioService.GetTreinoExercicioById(id);

                if (getTreinoExercicioId == null)
                {
                    return NotFound();
                }
                return Ok(getTreinoExercicioId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar treino exercicio");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TreinoExercicioCreateDTO>> CreateTreinoExercicio([FromBody] TreinoExercicioCreateDTO treinoExercicio)
        {
            try
            {
                var createTreinoExercicio = await _treinoExercicioService.CreateTreinoExercicio(treinoExercicio);
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar treino exercicio");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TreinoExercicio>> UpdateTreinoExercicio(int id, [FromBody] TreinoExercicio treinoExercicio)
        {
            try
            {
                if (id != treinoExercicio.Id)
                {
                    return BadRequest();
                }
                var updateTreinoExercicio = await _treinoExercicioService.UpdateTreinoExercicio(id, treinoExercicio);
                if (updateTreinoExercicio == null)
                {
                    return NotFound();
                }
                return Ok(updateTreinoExercicio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar treino exercicio");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TreinoExercicio>> DeleteTreinoExercicio(int id)
        {
            try
            {
                var deleteTreinoExercicio = await _treinoExercicioService.DeleteTreinoExercicio(id);
                if (!deleteTreinoExercicio)
                {
                    return NotFound();
                }
                return Ok(deleteTreinoExercicio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar treino exercicio");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
