using DevStudy.Application.DTOs.AvaliacaoFisica;
using DevStudy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Interfaces;

public interface IAvaliacaoFisicaService
{
    Task<IEnumerable<AvaliacaoFisica>> GetAvaliacoesFisicas();
    Task<AvaliacaoFisica> GetAvaliacaoFisica(int id);
    Task<AvaliacaoFisicaDTO> CreateAvaliacaoFisica(AvaliacaoFisicaDTO avaliacaoFisicaDTO);
    Task<AvaliacaoFisicaDTO> UpdateAvaliacaoFisica(int id, AvaliacaoFisicaDTO avaliacaoFisicaDTO);
    Task<bool> DeleteAvaliacaoFisica(int id);
}
