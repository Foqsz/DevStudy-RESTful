using DevStudy.Application.DTOs.Treino;
using DevStudy.Application.Interfaces;
using DevStudy.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevStudy.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreinosController : ControllerBase
    {
        private readonly ITreinosService _treinosService;
        private ILogger<TreinosController> _logger;

        public TreinosController(ITreinosService treinosService, ILogger<TreinosController> logger)
        {
            _treinosService = treinosService;
            _logger = logger;
        }

        /// <summary>
        /// Get all treinos
        /// </summary>
        /// <returns>List of TreinoDTO</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TreinoDTO>>> GetTreinos()
        {
            try
            {
                var treinosAll = await _treinosService.GetTreinos();
                if (!treinosAll.Any())
                {
                    _logger.LogError("Nenhum treino encontrado");
                    return NotFound();
                }
                return Ok(treinosAll);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar treinos");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar treinos");
            }
        }

        /// <summary>
        /// Get treino by id
        /// </summary>
        /// <param name="id">Treino id</param>
        /// <returns>TreinoDTO</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TreinoDTO>> GetTreinoById(int id)
        {
            try
            {
                var treinoId = await _treinosService.GetTreinoById(id);
                if (treinoId == null)
                {
                    _logger.LogError("Treino não encontrado");
                    return NotFound();
                }
                return Ok(treinoId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar treino");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar treino");
            }
        }

        /// <summary>
        /// Create a new treino
        /// </summary>
        /// <param name="treino">TreinoCreateDTO</param>
        /// <returns>Created TreinoCreateDTO</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TreinoCreateDTO>> CreateTreino([FromBody] TreinoCreateDTO treino)
        {
            try
            {
                var newTreino = await _treinosService.CreateTreino(treino);
                if (newTreino == null)
                {
                    _logger.LogError("Treino não criado");
                    return NotFound();
                }
                return Ok(newTreino);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar treino");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar treino");
            }
        }

        /// <summary>
        /// Update an existing treino
        /// </summary>
        /// <param name="id">Treino id</param>
        /// <param name="treinoDto">TreinoCreateDTO</param>
        /// <returns>Updated TreinoCreateDTO</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TreinoCreateDTO>> UpdateTreino(int id, [FromBody] TreinoCreateDTO treinoDto)
        {
            var treinoUpdate = await _treinosService.UpdateTreino(id, treinoDto);

            if (id != treinoDto.AlunoId)
            {
                return BadRequest("Id do treino não corresponde ao id do aluno");
            }
            return Ok(treinoUpdate);
        }

        /// <summary>
        /// Delete a treino by id
        /// </summary>
        /// <param name="id">Treino id</param>
        /// <returns>Boolean indicating success</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteTreino(int id)
        {
            var treinoDelete = await _treinosService.DeleteTreino(id);
            if (!treinoDelete)
            {
                _logger.LogError("Treino não deletado");
                return NotFound();
            }
            return Ok(treinoDelete);
        }
    }
}
