using DevStudy.Application.Interfaces;
using DevStudy.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevStudy.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly IPagamentoService _pagamentoService;
        private ILogger<PagamentoController> _logger;

        public PagamentoController(IPagamentoService pagamentoService, ILogger<PagamentoController> logger)
        {
            _pagamentoService = pagamentoService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os pagamentos do sistema.
        /// </summary>
        /// <returns>Lista de pagamentos.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtém todos os devedores", Description = "Retorna uma lista de todos os pagamentos.")]
        public async Task<ActionResult<IEnumerable<Pagamento>>> GetPagamentosAll()
        {
            try
            {
                var todosDevedores = await _pagamentoService.GetPagamentosAll();

                if (todosDevedores == null)
                {
                    _logger.LogError("CONTROLLER: Nenhum devedor encontrado.");
                    return NotFound("Nenhum devedor encontrado.");
                }
                return Ok(todosDevedores);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Obtém um extrato devedor pelo id.
        /// </summary>
        /// <returns>Lista um devedor pelo id.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Checa um extrato pelo ID.", Description = "Retorna um extrato pelo ID.")]
        public async Task<ActionResult<Pagamento>> GetPagamentoById(int id)
        {
            try
            {
                var devedorId = await _pagamentoService.GetPagamentoById(id);

                if (devedorId == null)
                {
                    _logger.LogError($"CONTROLLER: Devedor id={id} não encontrado.");
                    return NotFound($"Devedor id={id} não encontrado.");
                }

                return Ok(devedorId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Obtém os devedores de acordo com o informado pago ou pendente.
        /// </summary>
        /// <returns>Lista os devedores com status pago ou pendente.</returns>
        [HttpGet("{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Checa um extrato pelo status (PAGO/PENDENTE)", Description = "Retorna uma lista de pagamento Pendente/Pago.")]
        public async Task<ActionResult<Pagamento>> GetDevedoresStatus(string status)
        {
            try
            {
                if (status != "Pendente" && status != "Pago")
                {
                    _logger.LogError("Status diferente de Pago ou Pendente.");
                    return BadRequest($"Status informado={status}, diferente de Pago ou Pendente.");
                }

                var statusDevedor = await _pagamentoService.GetDevedores(status);

                if (statusDevedor == null)
                {
                    _logger.LogError("Nenhum devedor encontrado.");
                    return NotFound($"Nenhum devedor com status= {status} encontrado.");
                }

                return Ok(statusDevedor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Registra um pagamento no sistema.
        /// </summary>
        /// <returns>Retorna o cadastramento de um pagamento no sistema da academia.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Registrar um pagamento.", Description = "Retorna a criação de um pagamento Pendente/Pago.")]
        [HttpPost]
        public async Task<ActionResult<Pagamento>> CreatePagamento([FromBody] Pagamento pagamento)
        {
            try
            {
                if (pagamento.Status != "Pendente" && pagamento.Status != "Pago")
                {
                    _logger.LogError("Status diferente de Pago ou Pendente.");
                    return BadRequest($"Status informado={pagamento.Status}, diferente de Pago ou Pendente.");
                }

                var createPagamento = await _pagamentoService.CreatePagamento(pagamento);

                if (createPagamento == null)
                {
                    _logger.LogError($"Pagamento {pagamento} não cadastrado.");
                    return NotFound($"Pagamento {pagamento} não cadastrado.");
                }

                return CreatedAtAction(nameof(GetPagamentoById), new { id = createPagamento.Id }, createPagamento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Atualiza um pagamento.", Description = "Atualiza um pagamento existente.")]
        public async Task<ActionResult<Pagamento>> UpdatePagamento(int id, [FromBody] Pagamento pagamento)
        {
            try
            {
                if (id != pagamento.Id)
                {
                    _logger.LogError("Os IDs informados não são iguais.");
                    return BadRequest("Os IDs informados não são iguais.");
                }

                if (pagamento.Status != "Pendente" && pagamento.Status != "Pago")
                {
                    _logger.LogError("Status diferente de Pago ou Pendente.");
                    return BadRequest($"Status informado={pagamento.Status}, diferente de Pago ou Pendente.");
                }

                var updatePagamento = await _pagamentoService.UpdatePagamento(id, pagamento);

                if (updatePagamento == null)
                {
                    _logger.LogError($"Pagamento id={id} não encontrado.");
                    return NotFound($"Pagamento id={id} não encontrado.");
                }

                return Ok(updatePagamento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar pagamento.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Deleta um pagamento.", Description = "Deleta um pagamento existente pelo ID.")]
        public async Task<ActionResult<bool>> DeletePagamento(int id)
        {
            try
            {
                var deletarPagamento = await _pagamentoService.DeletePagamento(id);

                if (!deletarPagamento)
                {
                    _logger.LogError($"Pagamento id={id} não encontrado.");
                    return NotFound($"Pagamento id={id} não encontrado.");
                }

                return Ok(deletarPagamento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar pagamento.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
