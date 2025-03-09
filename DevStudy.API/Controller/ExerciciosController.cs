using DevStudy.Application.Interfaces;  
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevStudy.Domain.Models;

namespace DevStudy.API.Controller
{
    /// <summary>
    /// API Controller for managing Exercicios.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciciosController : ControllerBase
    {
        private readonly IExerciciosService _exerciciosService;
        private ILogger<ExerciciosController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExerciciosController"/> class.
        /// </summary>
        /// <param name="exerciciosService">The service for managing exercicios.</param>
        /// <param name="logger">The logger instance.</param>
        public ExerciciosController(IExerciciosService exerciciosService, ILogger<ExerciciosController> logger)
        {
            _exerciciosService = exerciciosService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all exercicios.
        /// </summary>
        /// <returns>A list of exercicios.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Exercicio>>> GetExercicios()
        {
            var exerciciosAll = await _exerciciosService.GetExercicios();

            if (exerciciosAll == null)
            {
                _logger.LogError("Nenhum exercicio localizado");
                return NotFound();
            }

            return Ok(exerciciosAll);
        }
        
        /// <summary>
        /// Gets an exercicio by its ID.
        /// </summary>
        /// <param name="id">The ID of the exercicio.</param>
        /// <returns>The exercicio with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Exercicio>> GetExercicioById(int id)
        {
            var exercicioId = await _exerciciosService.GetExercicioById(id);

            if (exercicioId == null)
            {
                _logger.LogError($"Exercicio com id {id} não localizado");
                return NotFound();
            }

            return Ok(exercicioId);
        }

        /// <summary>
        /// Creates a new exercicio.
        /// </summary>
        /// <param name="exercicio">The exercicio to create.</param>
        /// <returns>The created exercicio.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Exercicio>> CreateExercicio([FromBody] Exercicio exercicio)
        {
            var newExercicio = await _exerciciosService.CreateExercicio(exercicio);

            if (newExercicio == null)
            {
                _logger.LogError("Erro ao criar exercicio");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(newExercicio);
        }

        /// <summary>
        /// Updates an existing exercicio.
        /// </summary>
        /// <param name="id">The ID of the exercicio to update.</param>
        /// <param name="exercicio">The updated exercicio.</param>
        /// <returns>The updated exercicio.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Exercicio>> UpdateExercicio(int id, [FromBody] Exercicio exercicio)
        {
            var updateExercicio = await _exerciciosService.UpdateExercicio(id, exercicio);
            if (updateExercicio == null)
            {
                _logger.LogError($"Erro ao atualizar exercicio com id {id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(updateExercicio);
        }

        /// <summary>
        /// Deletes an exercicio by its ID.
        /// </summary>
        /// <param name="id">The ID of the exercicio to delete.</param>
        /// <returns>An action result.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteExercicio(int id)
        {
            var deleteExercicio = await _exerciciosService.DeleteExercicio(id);
            if (!deleteExercicio)
            {
                _logger.LogError($"Erro ao deletar exercicio com id {id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}
