using AutoMapper;
using DevStudy.Application.DTOs.Aluno;
using DevStudy.Application.Interfaces;
using DevStudy.Domain.Models;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller responsável por gerenciar as operações relacionadas aos alunos.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AlunoController : ControllerBase
{
    private readonly IAlunoService _alunoService;
    private ILogger<AlunoController> _logger;
    private IMapper _mapper;

    /// <summary>
    /// Construtor da classe AlunoController.
    /// </summary>
    /// <param name="alunoService">Serviço de aluno.</param>
    /// <param name="logger">Logger para registrar logs.</param>
    /// <param name="mapper">Mapper para mapeamento de objetos.</param>
    public AlunoController(IAlunoService alunoService, ILogger<AlunoController> logger, IMapper mapper)
    {
        _alunoService = alunoService;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtém todos os alunos cadastrados.
    /// </summary>
    /// <returns>Lista de alunos.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Obtém um aluno pelo seu ID.
    /// </summary>
    /// <param name="id">ID do aluno.</param>
    /// <returns>Dados do aluno.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Obtém um aluno pelo seu email.
    /// </summary>
    /// <param name="email">Email do aluno.</param>
    /// <returns>Dados do aluno.</returns>
    [HttpGet("{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Adiciona um novo aluno.
    /// </summary>
    /// <param name="aluno">Dados do aluno a ser adicionado.</param>
    /// <returns>Dados do aluno adicionado.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Atualiza os dados de um aluno.
    /// </summary>
    /// <param name="id">ID do aluno a ser atualizado.</param>
    /// <param name="aluno">Dados atualizados do aluno.</param>
    /// <returns>Dados do aluno atualizado.</returns>
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Deleta um aluno pelo seu ID.
    /// </summary>
    /// <param name="id">ID do aluno a ser deletado.</param>
    /// <returns>Status da operação de deleção.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
