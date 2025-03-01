using DevStudy.Application.DTOs.Treino;
using DevStudy.Application.Interfaces;
using DevStudy.Core.Models;
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

        [HttpGet]
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

        [HttpGet("{id:int}")]
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

        [HttpPost]
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

        [HttpPut]
        public async Task<ActionResult<TreinoCreateDTO>> UpdateTreino(int id, [FromBody] TreinoCreateDTO treinoDto)
        {
            var treinoUpdate = await _treinosService.UpdateTreino(id, treinoDto);

            if (id != treinoDto.AlunoId)
            {
                return BadRequest("Id do treino não corresponde ao id do aluno");
            }
            return Ok(treinoUpdate);
        }

        [HttpDelete]
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
