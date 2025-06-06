﻿using DevStudy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Interfaces;

public interface IPlanoService
{
    Task<IEnumerable<Plano>> GetPlanos();
    Task<Plano> GetPlano(int id);
    Task<Plano> CreatePlano(Plano plano);
    Task<Plano> UpdatePlano(int id, Plano plano);
    Task<bool> DeletePlano(int id);
}
