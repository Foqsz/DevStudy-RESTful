using AutoMapper;
using DevStudy.Application.Interfaces;
using DevStudy.Domain.Interfaces;
using DevStudy.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Services;

public class PagamentoService : IPagamentoService
{
    private readonly IPagamentoRepository _repository;
    private ILogger<PagamentoService> _logger;
    private IMapper _mapper;

    public PagamentoService(IPagamentoRepository repository, ILogger<PagamentoService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Pagamento>> GetPagamentosAll()
    {
        var todosDevedores = await _repository.GetPagamentosAll();

        if (!todosDevedores.Any())
        {
            _logger.LogError("Nenhum pagamento encontrado.");
            return null;
        }

        return todosDevedores;
    }

    public async Task<Pagamento> GetDevedores(string status)
    {
        if (status != "Pendente" && status != "Pago")
        {
            _logger.LogError("Status diferente de Pago ou Pendente.");
            return null;
        }

        return await _repository.GetDevedores(status);
    }

    public async Task<Pagamento> GetPagamentoById(int id)
    {
        var pagamentoId = await _repository.GetPagamentoById(id);

        if (pagamentoId == null)
        {
            _logger.LogError($"Nenhum pagamento com o id= {id} localizado.");
            return null;
        }

        return pagamentoId;
    }

    public async Task<Pagamento> CreatePagamento(Pagamento pagamento)
    {
        if (pagamento.Status != "Pendente" && pagamento.Status != "Pago")
        {
            _logger.LogError("Status diferente de Pago ou Pendente.");
            return null;
        }

        return await _repository.CreatePagamento(pagamento);
    } 

    public Task<Pagamento> UpdatePagamento(int id, Pagamento pagamento)
    {
        if (id != pagamento.Id)
        {
            _logger.LogError($"Id={id} informado diferente do pagamentoId={pagamento.Id}");
            return null;
        }

        if (pagamento.Status != "Pendente" && pagamento.Status != "Pago")
        {
            _logger.LogError("Status diferente de Pago ou Pendente.");
            return null;
        }

        return _repository.UpdatePagamento(id, pagamento);
    }

    public async Task<bool> DeletePagamento(int id)
    {
        var deletePagamento = await _repository.DeletePagamento(id);

        if (deletePagamento == true)
        {
            return true;
        }

        return false;
    }
}
