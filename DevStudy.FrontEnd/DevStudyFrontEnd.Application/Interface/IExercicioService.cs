using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;

public interface IExercicioService
{
    Task<IEnumerable<ExercicioViewModel>> GetExercicios();
    Task<ExercicioViewModel> GetExercicioById(int id);
    Task<ExercicioViewModel> CreateExercicio(ExercicioViewModel exercicio);
    Task<ExercicioViewModel> UpdateExercicio(int id, ExercicioViewModel exercicio);
    Task<bool> DeleteExercicio(int id);
}
