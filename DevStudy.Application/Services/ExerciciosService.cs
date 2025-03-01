using AutoMapper;
using DevStudy.Application.Interfaces;
using DevStudy.Domain.Models;
using DevStudy.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Services;

public class ExerciciosService : IExerciciosService
{
    private readonly IExerciciosRepository _exerciciosRepository;
    private ILogger<ExerciciosService> _logger;
    private IMapper _mapper;

    public ExerciciosService(IExerciciosRepository exerciciosRepository, ILogger<ExerciciosService> logger, IMapper mapper)
    {
        _exerciciosRepository = exerciciosRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Exercicio>> GetExercicios()
    {
        var treinosAll = await _exerciciosRepository.GetExercicios();

        if (!treinosAll.Any())
        {
            _logger.LogError("Nenhum exercicio localizado");
            return null;
        }

        return _mapper.Map<IEnumerable<Exercicio>>(treinosAll);
    }

    public async Task<Exercicio> GetExercicioById(int id)
    {
        var exercicioId = await _exerciciosRepository.GetExercicioById(id);

        if (exercicioId == null)
        {
            _logger.LogError($"Exercicio com id {id} não localizado");
            return null;
        }

        return exercicioId;
    }

    public async Task<Exercicio> CreateExercicio(Exercicio exercicio)
    {
        var newExercicio = await _exerciciosRepository.CreateExercicio(exercicio);

        if (newExercicio == null)
        {
            _logger.LogError("Erro ao criar exercicio");
            return null;
        }
        return newExercicio;
    }

    public async Task<Exercicio> UpdateExercicio(int id, Exercicio exercicio)
    {
        if (id != exercicio.Id)
        {
            _logger.LogError("Id do exercicio não corresponde");
            return null;
        }

        var updateExercicio = await _exerciciosRepository.UpdateExercicio(id, exercicio);

        if (updateExercicio == null)
        {
            _logger.LogError("Erro ao atualizar exercicio");
            return null;
        }

        return updateExercicio;
    }

    public async Task<bool> DeleteExercicio(int id)
    {
        var deleteExercicio = await _exerciciosRepository.DeleteExercicio(id);
        if (!deleteExercicio)
        {
            _logger.LogError("Erro ao deletar exercicio");
            return false;
        }
        return true;
    }
}
