using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;

public interface ITreinosService
{
    Task<IEnumerable<TreinoViewModel>> GetTreinos();
    Task<TreinoViewModel> GetTreinoById(int id);
    Task<TreinoViewModel> CreateTreino(TreinoViewModel treino);
    Task<TreinoViewModel> UpdateTreino(int id, TreinoViewModel treino);
    Task<bool> DeleteTreino(int id);
}
