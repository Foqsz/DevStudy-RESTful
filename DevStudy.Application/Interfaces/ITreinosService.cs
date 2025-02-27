using DevStudy.Application.DTOs.Treino;
using DevStudy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Interfaces;

public interface ITreinosService
{
    Task<IEnumerable<TreinoDTO>> GetTreinos();
    Task<TreinoDTO> GetTreinoById(int id);
    Task<TreinoCreateDTO> CreateTreino(TreinoCreateDTO treino);
    Task<Treino> UpdateTreino(int id, Treino treino);
    Task<bool> DeleteTreino(int id);
}
