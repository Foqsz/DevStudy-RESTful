using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;

public interface IAvaliacaoFisicaService
{
    Task<IEnumerable<AvaliacaoFisicaViewModel>> GetAvaliacoesFisicas();
    Task<AvaliacaoFisicaViewModel> GetAvaliacaoFisica(int id);
    Task<AvaliacaoFisicaViewModel> CreateAvaliacaoFisica(AvaliacaoFisicaViewModel avaliacaoFisica);
    Task<AvaliacaoFisicaViewModel> UpdateAvaliacaoFisica(int id, AvaliacaoFisicaViewModel avaliacaoFisica);
    Task<bool> DeleteAvaliacaoFisica(int id);
}
