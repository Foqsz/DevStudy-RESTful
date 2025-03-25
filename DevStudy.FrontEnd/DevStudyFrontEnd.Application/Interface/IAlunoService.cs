using DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Application.Interface;

public interface IAlunoService
{
    Task<IEnumerable<AlunoViewModel>> GetAlunos();
    Task<AlunoViewModel> GetAluno(int id);
    Task<AlunoViewModel> GetAlunoByEmail(string email);
    Task<AlunoViewModel> AddAluno(AlunoViewModel aluno);
    Task<AlunoViewModel> UpdateAluno(int id, AlunoViewModel aluno);
    Task<bool> DeleteAluno(int id);
}
