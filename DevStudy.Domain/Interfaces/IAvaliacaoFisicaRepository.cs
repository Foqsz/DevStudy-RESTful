using DevStudy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Domain.Interfaces;

public interface IAvaliacaoFisicaRepository
{
    Task<IEnumerable<AvaliacaoFisica>> GetAvaliacoesFisicas();
    Task<AvaliacaoFisica> GetAvaliacaoFisica(int id);
    Task<AvaliacaoFisica> CreateAvaliacaoFisica(AvaliacaoFisica avaliacaoFisica);
    Task<AvaliacaoFisica> UpdateAvaliacaoFisica(int id, AvaliacaoFisica avaliacaoFisica);
    Task<bool> DeleteAvaliacaoFisica(int id);
}
