using DevStudy.Domain.Interfaces;
using DevStudy.Domain.Models;
using DevStudy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Infrastructure.Repository;

public class AvaliacaoFisicaRepository : IAvaliacaoFisicaRepository
{
    private readonly DataBaseContext _context;
    private ILogger<AvaliacaoFisicaRepository> _logger;

    public AvaliacaoFisicaRepository(DataBaseContext context, ILogger<AvaliacaoFisicaRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<AvaliacaoFisica>> GetAvaliacoesFisicas()
    {
        return await _context.AvaliacoesFisicas.ToListAsync();

        //var avaliacoesParaSomar = await _context.AvaliacoesFisicas.ToListAsync();

        //var somarIMC = AvalicaoAll.Sum(a => a.Peso / (a.Altura * a.Altura));
        //_logger.LogInformation($"Total IMC: {somarIMC}");
        //return AvalicaoAll;
    }

    public async Task<AvaliacaoFisica> GetAvaliacaoFisica(int id)
    {
        return await _context.AvaliacoesFisicas.SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<AvaliacaoFisica> CreateAvaliacaoFisica(AvaliacaoFisica avaliacaoFisica)
    {
        var avaliacaoExist = await _context.AvaliacoesFisicas.FindAsync(avaliacaoFisica.Id);

        if (avaliacaoExist != null)
        {
            _logger.LogError("Avaliação já cadastrada com este ID");
            return null;
        }

        var alunoAvaliado = await _context.Alunos.FindAsync(avaliacaoFisica.AlunoId);

        if (alunoAvaliado == null)
        {
            _logger.LogError("Aluno não existente.");
            return null;
        }
         
        var avaliacaoAlunoExist = await _context.AvaliacoesFisicas
            .Where(a => a.AlunoId == avaliacaoFisica.AlunoId)
            .ToListAsync();

        if (avaliacaoAlunoExist.Any())
        {
            _logger.LogError("Avaliação já cadastrada para este aluno.");
            return null;
        }

        // Calcular o IMC
        var alunoIMC = avaliacaoFisica.Peso / (avaliacaoFisica.Altura * avaliacaoFisica.Altura);
        avaliacaoFisica.IMC = Math.Round(alunoIMC, 2);

        // Calcular a idade do aluno diretamente aqui
        var idade = DateTime.Now.Year - alunoAvaliado.DataNascimento.Year;
        if (alunoAvaliado.DataNascimento.Date > DateTime.Now.AddYears(-idade)) idade--; // Ajustar caso ainda não tenha feito aniversário este ano

        // Calcular o Percentual de Gordura usando a fórmula de Deurenberg
        var percentualGordura = (1.2m * alunoIMC) + (0.23m * idade) - 5.4m;
        avaliacaoFisica.PercentualGordura = Math.Round(percentualGordura, 2);

        if ((double)alunoIMC <= 18.5)
        {
            avaliacaoFisica.ClassificacaoIMC = "Abaixo do peso";
        }
        else if ((double)alunoIMC > 18.5 && (double)alunoIMC <= 24.9)
        {
            avaliacaoFisica.ClassificacaoIMC = "Peso normal";
        }
        else if ((double)alunoIMC > 24.9 && (double)alunoIMC <= 29.9)
        {
            avaliacaoFisica.ClassificacaoIMC = "Sobrepeso";
        }
        else if ((double)alunoIMC > 29.9 && (double)alunoIMC <= 34.9)
        {
            avaliacaoFisica.ClassificacaoIMC = "Obesidade I";
        }
        else if ((double)alunoIMC > 34.9 && (double)alunoIMC <= 39.9)
        {
            avaliacaoFisica.ClassificacaoIMC = "Obesidade II";
        }
        else
        {
            avaliacaoFisica.ClassificacaoIMC = "Obesidade III";
        }

        _context.AvaliacoesFisicas.Add(avaliacaoFisica);
        await _context.SaveChangesAsync();
        return avaliacaoFisica;
    }

    public async Task<AvaliacaoFisica> UpdateAvaliacaoFisica(int id, AvaliacaoFisica avaliacaoFisica)
    {
        var avaliacaoExist = await _context.AvaliacoesFisicas.FindAsync(id);

        if (avaliacaoExist == null)
        {
            _logger.LogError("Avaliação não existente.");
            return null;
        }

        if (avaliacaoExist.AlunoId != avaliacaoFisica.AlunoId)
        {
            _logger.LogError("Não é permitido alterar o AlunoId de uma avaliação.");
            return null;
        } 

        var imcAluno = avaliacaoFisica.Peso / (avaliacaoFisica.Altura * avaliacaoFisica.Altura);

        avaliacaoExist.Data = avaliacaoFisica.Data;
        avaliacaoExist.Peso = avaliacaoFisica.Peso;
        avaliacaoExist.Altura = avaliacaoFisica.Altura;
        avaliacaoExist.IMC = imcAluno; 

        _context.AvaliacoesFisicas.Update(avaliacaoExist);
        await _context.SaveChangesAsync();
        return avaliacaoExist;
    }

    public async Task<bool> DeleteAvaliacaoFisica(int id)
    {
        var deleteAvaliacao = await _context.AvaliacoesFisicas.FindAsync(id);

        if (deleteAvaliacao == null)
        {
            _logger.LogError("Avaliação não existente.");
            return false;
        }

        _context.AvaliacoesFisicas.Remove(deleteAvaliacao);
        await _context.SaveChangesAsync();
        return true;
    }
}
