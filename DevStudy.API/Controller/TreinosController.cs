using DevStudy.Application.DTOs.Treino;
using DevStudy.Application.Interfaces;
using DevStudy.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        /// Obter todos os treinos
        /// </summary>
        /// <returns>Lista de TreinoDTO</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obter todos os treinos", Description = "Retorna uma lista de todos os treinos")]
        public async Task<ActionResult<IEnumerable<TreinoDTO>>> GetTreinos()
        {
            try
            {
                var treinosAll = await _treinosService.GetTreinos();
                if (!treinosAll.Any())
                {
                    _logger.LogError("Nenhum treino encontrado");
                    return NotFound("Nenhum treino foi encontrado.");
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
        /// Obter treino por id
        /// </summary>
        /// <param name="id">Id do treino</param>
        /// <returns>TreinoDTO</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obter treino por id", Description = "Retorna um treino específico pelo id")]
        public async Task<ActionResult<TreinoDTO>> GetTreinoById(int id)
        {
            try
            {
                var treinoId = await _treinosService.GetTreinoById(id);
                if (treinoId == null)
                {
                    _logger.LogError("Treino não encontrado");
                    return NotFound($"Treino id={id} não foi localizado.");
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
        /// Criar um novo treino
        /// </summary>
        /// <param name="treino">TreinoCreateDTO</param>
        /// <returns>TreinoCreateDTO criado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Criar um novo treino", Description = "Cria um novo treino")]
        public async Task<ActionResult<TreinoCreateDTO>> CreateTreino([FromBody] TreinoCreateDTO treino)
        {
            try
            {
                var newTreino = await _treinosService.CreateTreino(treino);
                if (newTreino == null)
                {
                    _logger.LogError("Treino não criado");
                    return NotFound("Falha ao criar o treino.");
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
        /// Atualizar um treino existente
        /// </summary>
        /// <param name="id">Id do treino</param>
        /// <param name="treinoDto">TreinoCreateDTO</param>
        /// <returns>TreinoCreateDTO atualizado</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Atualizar um treino existente", Description = "Atualiza um treino existente")]
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
        /// Deletar um treino por id
        /// </summary>
        /// <param name="id">Id do treino</param>
        /// <returns>Boolean indicando sucesso</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Deletar um treino por id", Description = "Deleta um treino específico pelo id")]
        public async Task<ActionResult<bool>> DeleteTreino(int id)
        {
            var treinoDelete = await _treinosService.DeleteTreino(id);
            if (!treinoDelete)
            {
                _logger.LogError("Treino não deletado");
                return NotFound($"Treino id={id} não encontrado.");
            }
            return Ok(treinoDelete);
        }
    }
}
