using DevStudy.Application.Interfaces;
using DevStudy.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevStudy.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciciosController : ControllerBase
    {
        private readonly IExerciciosService _exerciciosService;
        private ILogger<ExerciciosController> _logger;

        public ExerciciosController(IExerciciosService exerciciosService, ILogger<ExerciciosController> logger)
        {
            _exerciciosService = exerciciosService;
            _logger = logger;
        }

        [HttpGet]
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

        [HttpGet("{id}")]
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

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
