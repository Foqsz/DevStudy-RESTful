using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;

public interface IPlanoService
{
    Task<IEnumerable<PlanoViewModel>> GetPlanos();
    Task<PlanoViewModel> GetPlano(int id);
    Task<PlanoViewModel> CreatePlano(PlanoViewModel plano);
    Task<PlanoViewModel> UpdatePlano(int id, PlanoViewModel plano);
    Task<bool> DeletePlano(int id);
}
