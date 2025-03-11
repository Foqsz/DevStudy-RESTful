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

public class PlanoService : IPlanoService
{
    private readonly IPlanoRepository _planoRepository;
    private ILogger<PlanoService> _loger;

    public PlanoService(IPlanoRepository planoRepository, ILogger<PlanoService> loger)
    {
        _planoRepository = planoRepository;
        _loger = loger;
    }

    public async Task<IEnumerable<Plano>> GetPlanos()
    {
        var planosAll = await _planoRepository.GetPlanos();

        if(!planosAll.Any())
        {
            _loger.LogError("Não existem planos cadastrados");
            return null;
        }

        return planosAll;
    }

    public async Task<Plano> GetPlano(int id)
    {
        var planoId = await _planoRepository.GetPlano(id);
        if (planoId == null)
        {
            _loger.LogError($"Não existe plano com o id={id}");
            return null;
        }

        return planoId;
    }

    public async Task<Plano> CreatePlano(Plano plano)
    {
        var newPlano = await _planoRepository.CreatePlano(plano);

        if (newPlano == null)
        {
            _loger.LogError("Erro ao cadastrar plano");
            return null;
        }

        return newPlano;
    }

    public async Task<Plano> UpdatePlano(int id, Plano plano)
    {
        var updatePlano = await _planoRepository.UpdatePlano(id, plano);

        if (updatePlano == null)
        {
            _loger.LogError("Erro ao atualizar o plano");
            return null;
        }

        return updatePlano;
    }

    public async Task<bool> DeletePlano(int id)
    {
        var deletePlano = await _planoRepository.DeletePlano(id);   

        if (deletePlano == null)
        {
            _loger.LogError($"Erro ao deletar o plano de id={id}");
        }

        return deletePlano;
    }   
}
