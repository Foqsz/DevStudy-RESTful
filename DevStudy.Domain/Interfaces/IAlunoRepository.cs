using DevStudy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Domain.Interfaces;

public interface IAlunoRepository
{
    Task<IEnumerable<Aluno>> GetAlunos();
    Task<Aluno> GetAluno(int id);
    Task<Aluno> GetAlunoByEmail(string email);
    Task<Aluno> CreateAluno(Aluno aluno);
    Task<Aluno> UpdateAluno(int id, Aluno aluno);
    Task<bool> DeleteAluno(int id);
}
