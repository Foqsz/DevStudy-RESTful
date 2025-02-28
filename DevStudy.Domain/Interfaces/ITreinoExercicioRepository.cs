using DevStudy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Domain.Interfaces;

public interface ITreinoExercicioRepository
{
    Task<IEnumerable<TreinoExercicio>> GetTreinoExercicios();
    Task<TreinoExercicio> GetTreinoExercicioById(int id);
    Task<TreinoExercicio> CreateTreinoExercicio(TreinoExercicio treinoExercicio);
    Task<TreinoExercicio> UpdateTreinoExercicio(int id, TreinoExercicio treinoExercicio);
    Task<bool> DeleteTreinoExercicio(int id);
}
