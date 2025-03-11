using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevStudy.Domain.Models;
using DevStudy.Domain.Interfaces;
using DevStudy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevStudy.Infrastructure.Repository;

public class AlunoRepository : IAlunoRepository
{
    private readonly DataBaseContext _context;
    private ILogger<AlunoRepository> _logger;

    public AlunoRepository(DataBaseContext context, ILogger<AlunoRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<IEnumerable<Aluno>> GetAlunos()
    {
        return await _context.Alunos
                             .Include(a => a.Treinos)  // Carrega os treinos
                                 .ThenInclude(t => t.Exercicios)  // Carrega os exercícios dentro de cada treino através da tabela TreinoExercicio
                                 .ThenInclude(te => te.Exercicio)  // Carrega o Exercicio associado ao TreinoExercicio 
                             .Include(a => a.Instrutor)  // Carrega o instrutor
                             .ToListAsync();
    }


    public async Task<Aluno> GetAluno(int id)
    {
        return await _context.Alunos.FindAsync(id);
    }

    public async Task<Aluno> GetAlunoByEmail(string email)
    {
        return await _context.Alunos.SingleOrDefaultAsync(a => a.Email == email);
    }

    public async Task<Aluno> CreateAluno(Aluno aluno)
    {
        var alunoExist = await _context.Alunos.AnyAsync(a => a.Id == aluno.Id);

        if (alunoExist)
        {
            _logger.LogError("Aluno já cadastrado com este ID");
            return null;
        }

        var emailExist = await _context.Alunos.AnyAsync(a => a.Email == aluno.Email);

        if (emailExist)
        {
            _logger.LogError("Email já cadastrado");
            return null;
        }

        var instrutorExist = await _context.Instrutores.AnyAsync(i => i.Id == aluno.InstrutorId);

        if (!instrutorExist)
        {
            _logger.LogError("Instrutor não existente.");
            return null;
        }

        _context.Alunos.Add(aluno);
        await _context.SaveChangesAsync();
        return aluno;
    }

    public async Task<Aluno> UpdateAluno(int id, Aluno aluno)
    {
        var alunoCheck = await _context.Alunos.FindAsync(id);
        if (alunoCheck == null)
        {
            _logger.LogError("Aluno não encontrado");
            return null;
        }

        if (id != aluno.Id)
        {
            _logger.LogError("ID do aluno não corresponde");
            return null;
        }

        var emailExist = await _context.Alunos
            .Where(a => a.Email == aluno.Email && a.Id != id) // Não considerar o aluno atual
            .AnyAsync(); // Usar AnyAsync() para verificar a existência

        if (emailExist)
        {
            _logger.LogError("Email já cadastrado");
            return null;
        }

        alunoCheck.Nome = aluno.Nome;
        alunoCheck.Email = aluno.Email;

        // Se a senha for fornecida (não nula ou vazia), atualize a senha
        if (!string.IsNullOrEmpty(aluno.Senha))
        {
            alunoCheck.Senha = aluno.Senha;
        }

        alunoCheck.Plano = aluno.Plano;
        alunoCheck.Ativo = aluno.Ativo;

        _context.Alunos.Update(alunoCheck);
        await _context.SaveChangesAsync();

        return alunoCheck;
    }

    public async Task<bool> DeleteAluno(int id)
    {
        var deleteAluno = await _context.Alunos.FindAsync(id);

        if (deleteAluno == null)
        {
            _logger.LogError("Aluno não encontrado");
            return false;
        }

        _context.Alunos.Remove(deleteAluno);
        await _context.SaveChangesAsync();

        return true;
    }
}
