using AutoMapper;
using DevStudy.Application.DTOs.TreinoExercicio;
using DevStudy.Application.Interfaces;
using DevStudy.Core.Models;
using DevStudy.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Services;

public class TreinoExercicioService : ITreinoExercicioService
{
    private readonly ITreinoExercicioRepository _treinoExercicioRepository;
    private ILogger<TreinoExercicioService> _logger;
    private IMapper _mapper;

    public TreinoExercicioService(ITreinoExercicioRepository treinoExercicioRepository, ILogger<TreinoExercicioService> logger, IMapper mapper)
    {
        _treinoExercicioRepository = treinoExercicioRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TreinoExercicio>> GetTreinoExercicios()
    {
        var treinosAll = await _treinoExercicioRepository.GetTreinoExercicios();

        if (!treinosAll.Any())
        {
            _logger.LogError("Nenhum treino exercicio localizado");
            return null;
        }

        return treinosAll;
    }

    public async Task<TreinoExercicio> GetTreinoExercicioById(int id)
    {
        var TreinoExercicioId = await _treinoExercicioRepository.GetTreinoExercicioById(id);

        if (TreinoExercicioId == null)
        {
            _logger.LogError($"TreinoExercicio com id {id} não localizado");
            return null;
        }
        return TreinoExercicioId;
    }


    public async Task<TreinoExercicioCreateDTO> CreateTreinoExercicio(TreinoExercicioCreateDTO treinoExercicio)
    {
        var treinoMappeado = _mapper.Map<TreinoExercicio>(treinoExercicio);

        var createTreinoExercicio = await _treinoExercicioRepository.CreateTreinoExercicio(treinoMappeado);

        if (createTreinoExercicio == null)
        {
            _logger.LogError("Erro ao criar treino exercicio");
            return null;
        }
        return _mapper.Map<TreinoExercicioCreateDTO>(createTreinoExercicio);
    }

    public async Task<TreinoExercicio> UpdateTreinoExercicio(int id, TreinoExercicio treinoExercicio)
    {
        if (id != treinoExercicio.Id)
        {
            _logger.LogError("Id do treino exercicio não corresponde");
            return null;
        }
        var updateTreinoExercicio = await _treinoExercicioRepository.UpdateTreinoExercicio(id, treinoExercicio);

        if (updateTreinoExercicio == null)
        {
            _logger.LogError("Erro ao atualizar treino exercicio");
            return null;
        }

        return updateTreinoExercicio;
    }

    public async Task<bool> DeleteTreinoExercicio(int id)
    {
        var deletTreinoExercicio = await _treinoExercicioRepository.DeleteTreinoExercicio(id);

        if (!deletTreinoExercicio)
        {
            _logger.LogError("Erro ao deletar treino exercicio");
            return false;
        }
        return true;
    }
}
