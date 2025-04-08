using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;

public interface IProfessorService
{
    Task<IEnumerable<InstrutorViewModel>> GetProfessores();
    Task<InstrutorViewModel> GetProfessorById(int id);
    Task<InstrutorViewModel> CreateProfessor(InstrutorViewModel professor);
    Task<InstrutorViewModel> UpdateProfessor(int id, InstrutorViewModel professor);
    Task<bool> DeleteProfessor(int id);
}
