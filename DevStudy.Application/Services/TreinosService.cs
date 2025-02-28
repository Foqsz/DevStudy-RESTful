using AutoMapper;
using DevStudy.Application.DTOs.Treino;
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

public class TreinosService : ITreinosService
{
    private readonly ITreinosRepository _treinos;
    private ILogger<TreinosService> _logger;
    private IMapper _mapper;

    public TreinosService(ITreinosRepository treinos, ILogger<TreinosService> logger, IMapper mapper)
    {
        _treinos = treinos;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<IEnumerable<TreinoDTO>> GetTreinos()
    {
        var treinos = await _treinos.GetTreinos();

        if (!treinos.Any())
        {
            _logger.LogError("Nenhum treino encontrado");
            return null;
        }

        return _mapper.Map<IEnumerable<TreinoDTO>>(treinos);
    }

    public async Task<TreinoDTO> GetTreinoById(int id)
    {
        var treinoId = await _treinos.GetTreinoById(id);

        if (treinoId == null)
        {
            _logger.LogError("Treino não encontrado");
            return null;
        }

        return _mapper.Map<TreinoDTO>(treinoId);
    }

    public async Task<TreinoCreateDTO> CreateTreino(TreinoCreateDTO treino)
    {
        var treinoMapper = _mapper.Map<TreinoCreateDTO, Treino>(treino);
        var newTreino = await _treinos.CreateTreino(treinoMapper);

        if (newTreino == null)
        {
            _logger.LogError("Treino não criado");
            return null;
        }

        return _mapper.Map<TreinoCreateDTO>(newTreino);
    }

    public async Task<Treino> UpdateTreino(int id, Treino treino)
    {
        var updateTreino = _treinos.UpdateTreino(id, treino);

        if (updateTreino == null)
        {
            _logger.LogError("Treino não atualizado");
        }

        return treino;
    }

    public async Task<bool> DeleteTreino(int id)
    {
        var deleteTreino = await _treinos.DeleteTreino(id);

        if (deleteTreino == false)
        {
            _logger.LogError("Treino não deletado");
            return false;
        }

        return true;
    }
}
