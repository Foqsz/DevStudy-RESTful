using DevStudy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Interfaces;

public interface IExerciciosService
{
    Task<IEnumerable<Exercicio>> GetExercicios();
    Task<Exercicio> GetExercicioById(int id);
    Task<Exercicio> CreateExercicio(Exercicio exercicio);
    Task<Exercicio> UpdateExercicio(int id, Exercicio exercicio);
    Task<bool> DeleteExercicio(int id);
}
