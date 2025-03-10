using AutoMapper;
using DevStudy.Application.DTOs.Instrutor;
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

public class InstrutorService : IInstrutorService
{
    private readonly IInstrutorRepository _instrutorRepository;
    private ILogger<InstrutorService> _logger;
    private IMapper _mapper;

    public InstrutorService(IInstrutorRepository instrutorRepository, ILogger<InstrutorService> logger, IMapper mapper)
    {
        _instrutorRepository = instrutorRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Instrutor>> GetInstrutores()
    {
        var instrutores = await _instrutorRepository.GetInstrutores();

        if (!instrutores.Any())
        {
            _logger.LogError("Instrutores não encontrados");
            return null;
        }

        return instrutores;
    }

    public async Task<Instrutor> GetInstrutor(int id)
    {
        var instrutorId = await _instrutorRepository.GetInstrutor(id);

        if (instrutorId == null)
        {
            _logger.LogError("Instrutor não encontrado");
            return null;
        }

        return instrutorId;
    }

    public async Task<Instrutor> GetInstrutorByEmail(string email)
    {
        var instrutorEmail = await _instrutorRepository.GetInstrutorByEmail(email);

        if (instrutorEmail == null)
        {
            _logger.LogError("Instrutor não encontrado");
            return null;
        }

        return instrutorEmail;
    }

    public async Task<InstrutorDTO> CreateInstrutor(InstrutorDTO instrutor)
    {
        var newInstrutor = _mapper.Map<InstrutorDTO, Instrutor>(instrutor);

        var instrutorCreated = await _instrutorRepository.CreateInstrutor(newInstrutor);

        if (instrutorCreated == null)
        {
            _logger.LogError("Instrutor não cadastrado");
            return null;
        }

        return _mapper.Map<InstrutorDTO>(instrutorCreated);
    }

    public async Task<InstrutorDTO> UpdateInstrutor(int id, InstrutorDTO instrutor)
    {
        var updateInstrutor = _mapper.Map<InstrutorDTO, Instrutor>(instrutor);

        var instrutorUpdated = await _instrutorRepository.UpdateInstrutor(id, updateInstrutor);

        if (instrutorUpdated == null)
        {
            _logger.LogError("Instrutor não atualizado");
            return null;
        }

        return _mapper.Map<InstrutorDTO>(instrutorUpdated);
    }

    public async Task<bool> DeleteInstrutor(int id)
    {
        var deleteInstrutor = await _instrutorRepository.DeleteInstrutor(id);

        if (!deleteInstrutor)
        {
            _logger.LogError("Instrutor não deletado");
            return false;
        }

        return true;
    }
}
