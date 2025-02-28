using DevStudy.Application.DTOs.TreinoExercicio;
using DevStudy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Interfaces;

public interface ITreinoExercicioService
{
    Task<IEnumerable<TreinoExercicio>> GetTreinoExercicios();
    Task<TreinoExercicio> GetTreinoExercicioById(int id);
    Task<TreinoExercicioCreateDTO> CreateTreinoExercicio(TreinoExercicioCreateDTO treinoExercicio);
    Task<TreinoExercicio> UpdateTreinoExercicio(int id, TreinoExercicio treinoExercicio);
    Task<bool> DeleteTreinoExercicio(int id);
}
