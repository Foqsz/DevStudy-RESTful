using DevStudy.Application.Interfaces;
using DevStudy.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pagamento>>> GetPagamentosAll()
        {
            var todosDevedores = await _pagamentoService.GetPagamentosAll();

            if (todosDevedores == null)
            {
                _logger.LogError("CONTROLLER: Nenhum devedor encontrado.");
                return BadRequest("Nenhum devedor encontrado.");
            }

            return Ok(todosDevedores);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pagamento>> GetPagamentoById(int id)
        {
            var devedorId = await _pagamentoService.GetPagamentoById(id);

            if (devedorId == null)
            {
                _logger.LogError($"CONTROLLER: Devedor id={id} não encontrado.");
                return BadRequest($"Devedor id={id} não encontrado.");
            }

            return Ok(devedorId);
        }

        [HttpGet("{status}")]
        public async Task<ActionResult<Pagamento>> GetDevedoresStatus(string status)
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
                return BadRequest($"Nenhum devedor com status= {status} encontrado.");
            }

            return Ok(statusDevedor);
        }

        [HttpPost]
        public async Task<ActionResult<Pagamento>> CreatePagamento([FromBody] Pagamento pagamento)
        {
            if (pagamento.Status != "Pendente" && pagamento.Status != "Pago")
            {
                _logger.LogError("Status diferente de Pago ou Pendente.");
                return BadRequest($"Status informado={pagamento.Status}, diferente de Pago ou Pendente.");
            }

            var createPagamento = await _pagamentoService.CreatePagamento(pagamento);

            return CreatedAtAction(nameof(GetPagamentoById), new { id  = createPagamento.Id }, createPagamento);
        }

        [HttpPut]
        public async Task<ActionResult<Pagamento>> UpdatePagamento(int id, [FromBody] Pagamento pagamento)
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

            return updatePagamento;
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeletePagamento(int id)
        {
            var deletarPagamento = await _pagamentoService.DeletePagamento(id);

            if (!deletarPagamento)
            {
                _logger.LogError("Pagamento.");
                return BadRequest("Não foi possível deletar o ID informado. Não localizado.");
            }

            return Ok(deletarPagamento);    
        }
    }
}
