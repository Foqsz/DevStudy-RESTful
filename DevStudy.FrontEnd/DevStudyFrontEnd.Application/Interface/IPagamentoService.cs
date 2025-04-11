using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;

public interface IPagamentoService
{
    Task<IEnumerable<PagamentoViewModel>> GetPagamentos();
    Task<PagamentoViewModel> GetPagamento(int id);
    Task<PagamentoViewModel> CreatePagamento(PagamentoViewModel pagamento);
    Task<PagamentoViewModel> UpdatePagamento(int id, PagamentoViewModel pagamento);
    Task<bool> DeletePagamento(int id);
}
