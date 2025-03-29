using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;

public interface ITreinoExercicioService
{
    Task<IEnumerable<TreinoExercicioViewModel>> GetTreinoExercicios();
    Task<TreinoExercicioViewModel> GetTreinoExercicioById(int id);
    Task<TreinoExercicioViewModel> CreateTreinoExercicio(TreinoExercicioViewModel treinoExercicio);
    Task<TreinoExercicioViewModel> UpdateTreinoExercicio(int id, TreinoExercicioViewModel treinoExercicio);
    Task<bool> DeleteTreinoExercicio(int id);
}
