using DevStudy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Domain.Interfaces;

public interface ITreinosRepository
{
    Task<IEnumerable<Treino>> GetTreinos();
    Task<Treino> GetTreinoById(int id);
    Task<Treino> CreateTreino(Treino treino);
    Task<Treino> UpdateTreino(int id, Treino treino);
    Task<bool> DeleteTreino(int id); 
}
