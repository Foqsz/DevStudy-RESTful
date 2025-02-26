using DevStudy.Application.DTOs;
using DevStudy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Interfaces;

public interface IAlunoService
{
    Task<IEnumerable<AlunoDTO>> GetAlunos();
    Task<AlunoDTO> GetAluno(int id);
    Task<AlunoDTO> GetAlunoByEmail(string email);
    Task<AlunoCreateDTO> AddAluno(AlunoCreateDTO aluno);
    Task<AlunoUpdateDTO> UpdateAluno(int id, AlunoUpdateDTO aluno);
    Task<bool> DeleteAluno(int id);
}
