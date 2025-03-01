using AutoMapper;
using DevStudy.Application.DTOs.Aluno;
using DevStudy.Application.Interfaces;
using DevStudy.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevStudy.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;
        private ILogger<AlunoController> _logger;
        private IMapper _mapper;

        public AlunoController(IAlunoService alunoService, ILogger<AlunoController> logger, IMapper mapper)
        {
            _alunoService = alunoService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                if (alunos == null)
                {
                    _logger.LogError("Não há alunos cadastrados");
                    return NotFound();
                }
                return Ok(alunos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar obter todos os alunos");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter todos os alunos");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AlunoDTO>> GetAluno(int id)
        {
            try
            {
                var alunoId = await _alunoService.GetAluno(id);
                if (alunoId == null)
                {
                    _logger.LogError("Aluno não encontrado");
                    return NotFound();
                }
                return Ok(alunoId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar obter o aluno");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter o aluno");
            }
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<AlunoDTO>> GetAlunoByEmail(string email)
        {
            try
            {
                var alunoEmail = await _alunoService.GetAlunoByEmail(email);
                if (alunoEmail == null)
                {
                    _logger.LogError("Aluno não encontrado");
                    return NotFound();
                }
                return Ok(alunoEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar obter o aluno");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter o aluno");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AlunoCreateDTO>> AddAluno([FromBody] AlunoCreateDTO aluno)
        {
            try
            { 
                var newAluno = await _alunoService.AddAluno(aluno);
                var alunoDTO = _mapper.Map<AlunoCreateDTO, Aluno>(newAluno);
                return CreatedAtAction(nameof(GetAluno), new { id = alunoDTO.Id }, alunoDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar adicionar um novo aluno");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar adicionar um novo aluno");
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<AlunoUpdateDTO>> UpdateAluno(int id, [FromBody] AlunoUpdateDTO aluno)
        {
            try
            {
                if (id != aluno.Id)
                {
                    _logger.LogError("Id do aluno não corresponde");
                    return BadRequest();
                }
                var updateAluno = await _alunoService.UpdateAluno(id, aluno);
                if (updateAluno == null)
                {
                    _logger.LogError("Aluno não encontrado");
                    return NotFound();
                }
                return Ok(updateAluno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar atualizar o aluno");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar atualizar o aluno");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AlunoDTO>> DeleteAluno(int id)
        {
            try
            {
                var deleteAluno = await _alunoService.DeleteAluno(id);
                if (!deleteAluno)
                {
                    _logger.LogError("Aluno não encontrado");
                    return NotFound();
                }
                return Ok(deleteAluno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar deletar o aluno");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar deletar o aluno");
            }
        }
    }
}
