
using AutoMapper;
using DevStudy.Application.DTOs.Instrutor;
using DevStudy.Domain.Interfaces;
using DevStudy.Domain.Models;
using DevStudy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Infrastructure.Repository;

public class InstrutorRepository : IInstrutorRepository
{
    private readonly DataBaseContext _context;
    private ILogger<InstrutorRepository> _logger;
    private IMapper _mapper;

    public InstrutorRepository(DataBaseContext context, ILogger<InstrutorRepository> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Instrutor>> GetInstrutores()
    {
        return await _context.Instrutores.Include(a => a.Alunos).ToListAsync();
    }

    public async Task<Instrutor> GetInstrutor(int id)
    {
        return await _context.Instrutores.Include(a => a.Alunos)
                                         .ThenInclude(a => a.Treinos)
                                         .ThenInclude(a => a.Exercicios)
                                         .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Instrutor> GetInstrutorByEmail(string email)
    {
        return await _context.Instrutores.SingleOrDefaultAsync(a => a.Email == email);
    }

    public async Task<Instrutor> CreateInstrutor(Instrutor instrutor)
    {
        var emailExist = await _context.Instrutores.AnyAsync(a => a.Email == instrutor.Email);

        if (emailExist)
        {
            _logger.LogError("Email já cadastrado");
            return null;
        }

        var phoneExist = await _context.Instrutores.AnyAsync(i => i.Telefone == instrutor.Telefone);

        if (phoneExist)
        {
            _logger.LogError("Telefone já cadastrado");
            return null;
        }

        _context.Instrutores.Add(instrutor);
        await _context.SaveChangesAsync();
        return instrutor;
    }

    public async Task<Instrutor> UpdateInstrutor(int id, Instrutor instrutor)
    {
        var instrutorExist = await _context.Instrutores.AnyAsync(i => i.Id == id);

        if (!instrutorExist)
        {
            _logger.LogError("Instrutor não encontrado");
            return null;
        }

        var emailExist = await _context.Instrutores.AnyAsync(i => i.Email == instrutor.Email && i.Id != id);
        if (emailExist)
        {
            _logger.LogError("Email já cadastrado");
            return null;
        }

        var phoneExist = await _context.Instrutores.AnyAsync(i => i.Telefone == instrutor.Telefone && i.Id != id);
        if (phoneExist)
        {
            _logger.LogError("Telefone já cadastrado");
            return null;
        }

        _context.Instrutores.Update(instrutor);
        await _context.SaveChangesAsync();
        return instrutor;
    }

    public async Task<bool> DeleteInstrutor(int id)
    {
        var deleteInstrutor = await _context.Instrutores.FindAsync(id);

        if (deleteInstrutor == null)
        {
            _logger.LogError("Instrutor não encontrado");
            return false;
        }

        _context.Instrutores.Remove(deleteInstrutor);
        _context.SaveChanges();
        return true;
    }
}
