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

        /// <summary>
        /// Get all TreinoExercicios
        /// </summary>
        /// <returns>List of TreinoExercicios</returns>
        /// <response code="200">Returns the list of TreinoExercicios</response>
        /// <response code="404">If no TreinoExercicios are found</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Get a specific TreinoExercicio by ID
        /// </summary>
        /// <param name="id">TreinoExercicio ID</param>
        /// <returns>TreinoExercicio</returns>
        /// <response code="200">Returns the TreinoExercicio</response>
        /// <response code="404">If the TreinoExercicio is not found</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Create a new TreinoExercicio
        /// </summary>
        /// <param name="treinoExercicio">TreinoExercicio to create</param>
        /// <returns>Created TreinoExercicio</returns>
        /// <response code="201">Returns the newly created TreinoExercicio</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TreinoExercicioCreateDTO>> CreateTreinoExercicio([FromBody] TreinoExercicioCreateDTO treinoExercicio)
        {
            try
            {
                var createTreinoExercicio = await _treinoExercicioService.CreateTreinoExercicio(treinoExercicio);
                return CreatedAtAction(nameof(GetTreinoExercicioById), new { id = createTreinoExercicio.Id }, createTreinoExercicio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar treino exercicio");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update an existing TreinoExercicio
        /// </summary>
        /// <param name="id">TreinoExercicio ID</param>
        /// <param name="treinoExercicio">Updated TreinoExercicio</param>
        /// <returns>Updated TreinoExercicio</returns>
        /// <response code="200">Returns the updated TreinoExercicio</response>
        /// <response code="400">If the ID does not match the TreinoExercicio ID</response>
        /// <response code="404">If the TreinoExercicio is not found</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Delete a specific TreinoExercicio by ID
        /// </summary>
        /// <param name="id">TreinoExercicio ID</param>
        /// <returns>Deleted TreinoExercicio</returns>
        /// <response code="200">Returns the deleted TreinoExercicio</response>
        /// <response code="404">If the TreinoExercicio is not found</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
