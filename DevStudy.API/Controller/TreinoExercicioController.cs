using DevStudy.Application.DTOs.TreinoExercicio;
using DevStudy.Application.Interfaces; 
using DevStudy.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        /// Obter todos os TreinoExercicios
        /// </summary>
        /// <returns>Lista de TreinoExercicios</returns>
        /// <response code="200">Retorna a lista de TreinoExercicios</response>
        /// <response code="404">Se nenhum TreinoExercicio for encontrado</response>
        /// <response code="500">Se houver um erro interno no servidor</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obter todos os TreinoExercicios", Description = "Retorna uma lista de todos os TreinoExercicios")]
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
        /// Obter um TreinoExercicio específico pelo ID
        /// </summary>
        /// <param name="id">ID do TreinoExercicio</param>
        /// <returns>TreinoExercicio</returns>
        /// <response code="200">Retorna o TreinoExercicio</response>
        /// <response code="404">Se o TreinoExercicio não for encontrado</response>
        /// <response code="500">Se houver um erro interno no servidor</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obter um TreinoExercicio específico pelo ID", Description = "Retorna um TreinoExercicio pelo ID")]
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
        /// Criar um novo TreinoExercicio
        /// </summary>
        /// <param name="treinoExercicio">TreinoExercicio a ser criado</param>
        /// <returns>TreinoExercicio criado</returns>
        /// <response code="201">Retorna o TreinoExercicio recém-criado</response>
        /// <response code="500">Se houver um erro interno no servidor</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Criar um novo TreinoExercicio", Description = "Cria um novo TreinoExercicio")]
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
        /// Atualizar um TreinoExercicio existente
        /// </summary>
        /// <param name="id">ID do TreinoExercicio</param>
        /// <param name="treinoExercicio">TreinoExercicio atualizado</param>
        /// <returns>TreinoExercicio atualizado</returns>
        /// <response code="200">Retorna o TreinoExercicio atualizado</response>
        /// <response code="400">Se o ID não corresponder ao ID do TreinoExercicio</response>
        /// <response code="404">Se o TreinoExercicio não for encontrado</response>
        /// <response code="500">Se houver um erro interno no servidor</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Atualizar um TreinoExercicio existente", Description = "Atualiza um TreinoExercicio existente")]
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
        /// Deletar um TreinoExercicio específico pelo ID
        /// </summary>
        /// <param name="id">ID do TreinoExercicio</param>
        /// <returns>TreinoExercicio deletado</returns>
        /// <response code="200">Retorna o TreinoExercicio deletado</response>
        /// <response code="404">Se o TreinoExercicio não for encontrado</response>
        /// <response code="500">Se houver um erro interno no servidor</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Deletar um TreinoExercicio específico pelo ID", Description = "Deleta um TreinoExercicio pelo ID")]
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
