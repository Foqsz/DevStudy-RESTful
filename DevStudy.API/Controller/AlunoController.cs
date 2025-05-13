using AutoMapper;
using DevStudy.Application.DTOs.Aluno;
using DevStudy.Application.Interfaces;
using DevStudy.Domain.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(Summary = "Obtém todos os alunos cadastrados", Description = "Retorna uma lista de todos os alunos cadastrados no sistema.")]
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
    [SwaggerOperation(Summary = "Obtém um aluno pelo seu ID", Description = "Retorna os dados de um aluno específico pelo seu ID.")]
    public async Task<ActionResult<AlunoDTO>> GetAluno(int id)
    {
        try
        {
            var alunoId = await _alunoService.GetAluno(id);
            if (alunoId == null)
            {
                throw new GymExceptions("O aluno solicitado não foi encontrado.");
            } 
            return Ok(alunoId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao tentar obter o aluno");
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
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
    [SwaggerOperation(Summary = "Obtém um aluno pelo seu email", Description = "Retorna os dados de um aluno específico pelo seu email.")]
    public async Task<ActionResult<AlunoDTO>> GetAlunoByEmail(string email)
    {
        try
        {
            var alunoEmail = await _alunoService.GetAlunoByEmail(email);
            if (alunoEmail == null)
            {
                _logger.LogError("Aluno não encontrado");
                return NotFound($"Aluno com email {email} não localizado.");
            }
            return Ok(alunoEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao tentar obter o aluno");
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter o aluno");
        }
    }

    [HttpGet("treinos/imprimirTreino")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Gera um PDF com a lista de treinos dos alunos", Description = "Retorna um arquivo PDF contendo os nomes dos alunos e seus respectivos treinos.")]
    public async Task<ActionResult> GerarTreinosPdf(int idAluno)
    {
        try
        {
            var alunoComTreino = await _alunoService.GetAluno(idAluno);  

            if (alunoComTreino == null || alunoComTreino.Treinos == null || !alunoComTreino.Treinos.Any())
            {
                _logger.LogError("Aluno ou treinos não encontrados");
                return NotFound("Aluno ou treinos não encontrados.");
            }

            using var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            document.Add(new Paragraph($"Lista de Treinos do Aluno {alunoComTreino.Nome}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(18)
                .SetMarginBottom(20));

            document.Add(new Paragraph($"Aluno: {alunoComTreino.Nome} ID:{alunoComTreino.Id}")
                .SetFontSize(14));

            document.Add(new Paragraph($"Professor: {alunoComTreino.Instrutor.Nome}")
                .SetFontSize(14));

            document.Add(new Paragraph($"Data: {DateTime.Now}")
                .SetFontSize(14));

            document.Add(new Paragraph("[VV GYM] - LISTA DE EXÉRCICIOS:")
                .SetFontSize(14)
                .SetMarginTop(10));

            var lista = new List();
            foreach (var exercicio in alunoComTreino.Treinos)
            {
                lista.Add(new ListItem($"{exercicio.Exercicio.Nome} - {exercicio.Series} séries de {exercicio.Repeticoes} repetições"));
            }

            document.Add(lista);
            document.Close();

            var bytes = stream.ToArray();
            return File(bytes, "application/pdf", $"treinos-aluno-{alunoComTreino.Nome}.pdf");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao gerar PDF de treinos");
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao gerar PDF");
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
    [SwaggerOperation(Summary = "Adiciona um novo aluno", Description = "Adiciona um novo aluno ao sistema.")]
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
    [SwaggerOperation(Summary = "Atualiza os dados de um aluno", Description = "Atualiza os dados de um aluno específico pelo seu ID.")]
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
    [SwaggerOperation(Summary = "Deleta um aluno pelo seu ID", Description = "Deleta um aluno específico pelo seu ID.")]
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
