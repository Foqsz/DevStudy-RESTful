﻿using DevStudy.Application.DTOs.Treino;
using DevStudy.Domain.Models;
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
    Task<TreinoCreateDTO> UpdateTreino(int id, TreinoCreateDTO treino);
    Task<bool> DeleteTreino(int id);
}
