using DevStudy.Domain.Interfaces;
using DevStudy.Domain.Models;
using DevStudy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Infrastructure.Repository;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly DataBaseContext _dataBaseContext;
    private ILogger<PagamentoRepository> _logger;

    public PagamentoRepository(DataBaseContext dataBaseContext, ILogger<PagamentoRepository> logger)
    {
        _dataBaseContext = dataBaseContext;
        _logger = logger;
    }

    public async Task<IEnumerable<Pagamento>> GetPagamentosAll()
    {
        return await _dataBaseContext.Pagamentos.AsNoTracking().ToListAsync();
    }

    public async Task<Pagamento> GetDevedores(string status)
    {
        return await _dataBaseContext.Pagamentos.FindAsync(status);
    }

    public async Task<Pagamento> GetPagamentoById(int id)
    {
        return await _dataBaseContext.Pagamentos.SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Pagamento> CreatePagamento(Pagamento pagamento)
    {
        var cobrancaDuplicada = await _dataBaseContext.Pagamentos.AnyAsync(a => a.AlunoId == pagamento.AlunoId);

        if (cobrancaDuplicada)
        {
            _logger.LogError($"O aluno id={pagamento.AlunoId} já tem uma cobrança registrada.");
            return null;
        }

        var alunoExist = await _dataBaseContext.Alunos.AnyAsync(a => pagamento.AlunoId == a.Id);

        if (!alunoExist)
        {
            _logger.LogError($"O aluno id={pagamento.AlunoId} não foi localizado.");
            return null;
        }

        _dataBaseContext.Pagamentos.Add(pagamento);
        await _dataBaseContext.SaveChangesAsync();
        return pagamento;
    }

    public async Task<Pagamento> UpdatePagamento(int id, Pagamento pagamento)
    {
        var updatePagamento = await _dataBaseContext.Pagamentos.FindAsync(id);

        if (updatePagamento == null)
        {
            _logger.LogError("Pagamento não encontrado");
            return null;
        }

        updatePagamento.FormaPagamento = pagamento.FormaPagamento;
        updatePagamento.Status = pagamento.Status;
        updatePagamento.Valor = pagamento.Valor;
        updatePagamento.DataPagamento = pagamento.DataPagamento;
        updatePagamento.DataVencimento = pagamento.DataVencimento;
 
        _dataBaseContext.Pagamentos.Update(updatePagamento);
        await _dataBaseContext.SaveChangesAsync();
        return pagamento;
    }

    public async Task<bool> DeletePagamento(int id)
    {
        var deletePagamento = await _dataBaseContext.Pagamentos.FindAsync(id);

        if (deletePagamento == null)
        {
            _logger.LogError($"Pagamento id={id} não localizado.");
            return false;
        }

        _dataBaseContext.Remove(deletePagamento);
        await _dataBaseContext.SaveChangesAsync();
        return true;
    } 
}
