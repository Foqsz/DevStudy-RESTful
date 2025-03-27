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

    public async Task<IEnumerable<InstrutorDTO>> GetInstrutores()
    {
        var instrutores = await _instrutorRepository.GetInstrutores();

        if (!instrutores.Any())
        {
            _logger.LogError("Instrutores não encontrados");
            return null;
        }

        return _mapper.Map<IEnumerable<InstrutorDTO>>(instrutores);
    }

    public async Task<InstrutorDTO> GetInstrutor(int id)
    {
        var instrutorId = await _instrutorRepository.GetInstrutor(id);

        if (instrutorId == null)
        {
            _logger.LogError("Instrutor não encontrado");
            return null;
        }

        return _mapper.Map<InstrutorDTO>(instrutorId);
    }

    public async Task<InstrutorDTO> GetInstrutorByEmail(string email)
    {
        var instrutorEmail = await _instrutorRepository.GetInstrutorByEmail(email);

        if (instrutorEmail == null)
        {
            _logger.LogError("Instrutor não encontrado");
            return null;
        }

        return _mapper.Map<InstrutorDTO>(instrutorEmail);
    }

    public async Task<InstrutorCreateDTO> CreateInstrutor(InstrutorCreateDTO instrutor)
    {
        var newInstrutor = _mapper.Map<InstrutorCreateDTO, Instrutor>(instrutor);

        var instrutorCreated = await _instrutorRepository.CreateInstrutor(newInstrutor);

        if (instrutorCreated == null)
        {
            _logger.LogError("Instrutor não cadastrado");
            return null;
        }

        return _mapper.Map<InstrutorCreateDTO>(instrutorCreated);
    }

    public async Task<InstrutorCreateDTO> UpdateInstrutor(int id, InstrutorCreateDTO instrutor)
    {
        var updateInstrutor = _mapper.Map<InstrutorCreateDTO, Instrutor>(instrutor);

        var instrutorUpdated = await _instrutorRepository.UpdateInstrutor(id, updateInstrutor);

        if (instrutorUpdated == null)
        {
            _logger.LogError("Instrutor não atualizado");
            return null;
        }

        return _mapper.Map<InstrutorCreateDTO>(instrutorUpdated);
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
