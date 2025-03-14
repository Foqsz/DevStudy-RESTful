using DevStudy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Interfaces;

public interface IPagamentoService
{
    Task<IEnumerable<Pagamento>> GetPagamentosAll();
    Task<Pagamento> GetPagamentoById(int id);
    Task<Pagamento> GetDevedores(string status);
    Task<Pagamento> CreatePagamento(Pagamento pagamento);
    Task<Pagamento> UpdatePagamento(int id, Pagamento pagamento);
    Task<bool> DeletePagamento(int id);
}
