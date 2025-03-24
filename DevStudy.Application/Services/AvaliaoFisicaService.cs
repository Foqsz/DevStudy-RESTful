using AutoMapper;
using DevStudy.Application.DTOs.AvaliacaoFisica;
using DevStudy.Application.Interfaces;
using DevStudy.Domain.Interfaces;
using DevStudy.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Services;

public class AvaliacaoFisicaService : IAvaliacaoFisicaService
{
    private readonly IAvaliacaoFisicaRepository _avaliacaoFisicaService;
    private ILogger<AvaliacaoFisicaService> _logger;
    private IMapper _mapper;

    public AvaliacaoFisicaService(IAvaliacaoFisicaRepository avaliacaoFisicaService, ILogger<AvaliacaoFisicaService> logger, IMapper mapper)
    {
        _avaliacaoFisicaService = avaliacaoFisicaService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AvaliacaoFisica>> GetAvaliacoesFisicas()
    {
        var avaliacoes = await _avaliacaoFisicaService.GetAvaliacoesFisicas();

        if (!avaliacoes.Any())
        {
            _logger.LogError("Nenhuma avaliação física encontrada.");
            return null;
        }

        return avaliacoes;
    }

    public async Task<AvaliacaoFisica> GetAvaliacaoFisica(int id)
    {
        var avaliacaoId = await _avaliacaoFisicaService.GetAvaliacaoFisica(id);

        if (avaliacaoId == null)
        {
            _logger.LogError("Avaliação física não encontrada.");
            return null;
        }

        return avaliacaoId;
    }

    public async Task<AvaliacaoFisicaDTO> CreateAvaliacaoFisica(AvaliacaoFisicaDTO avaliacaoFisicaDTO)
    {
        var newAvaliacaoMapper = _mapper.Map<AvaliacaoFisica>(avaliacaoFisicaDTO);

        var newAvaliacao = await _avaliacaoFisicaService.CreateAvaliacaoFisica(newAvaliacaoMapper);

        if (newAvaliacao == null)
        {
            _logger.LogError("Erro ao criar avaliação física.");
            return null;
        }

        return _mapper.Map<AvaliacaoFisicaDTO>(newAvaliacao);
    }

    public async Task<AvaliacaoFisicaDTO> UpdateAvaliacaoFisica(int id, AvaliacaoFisicaDTO avaliacaoFisicaDTO)
    {
        var updateAvaliacaoMapper = _mapper.Map<AvaliacaoFisica>(avaliacaoFisicaDTO);

        var updateAvaliacao = await _avaliacaoFisicaService.UpdateAvaliacaoFisica(id, updateAvaliacaoMapper);

        if (updateAvaliacao == null)
        {
            _logger.LogError("Erro ao atualizar avaliação física.");
            return null;
        }

        return _mapper.Map<AvaliacaoFisicaDTO>(updateAvaliacao);
    }

    public async Task<bool> DeleteAvaliacaoFisica(int id)
    {
        var deleteAvaliacao = await _avaliacaoFisicaService.DeleteAvaliacaoFisica(id);

        if (!deleteAvaliacao)
        {
            _logger.LogError("Erro ao deletar avaliação física.");
            return false;
        }

        return true;
    }
}
