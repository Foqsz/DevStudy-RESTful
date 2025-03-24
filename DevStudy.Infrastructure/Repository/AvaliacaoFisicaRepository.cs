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

        var alunoIMC = avaliacaoFisica.Peso / (avaliacaoFisica.Altura * avaliacaoFisica.Altura);

        avaliacaoFisica.IMC = alunoIMC;

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
