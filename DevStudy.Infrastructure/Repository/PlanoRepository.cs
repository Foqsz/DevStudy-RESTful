using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevStudy.Domain.Interfaces;
using DevStudy.Domain.Models;
using DevStudy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevStudy.Infrastructure.Repository;

public class PlanoRepository : IPlanoRepository
{
    private readonly DataBaseContext _context;
    private ILogger<PlanoRepository> _logger;

    public PlanoRepository(DataBaseContext context, ILogger<PlanoRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Plano>> GetPlanos()
    {
        return await _context.Planos.AsNoTracking().ToListAsync();
    }

    public async Task<Plano> GetPlano(int id)
    {
        return await _context.Planos.FindAsync(id);
    }

    public async Task<Plano> CreatePlano(Plano plano)
    {
        var nomeExist = await _context.Planos.AnyAsync(p => p.Nome == plano.Nome);

        if (nomeExist)
        {
            _logger.LogError("Plano com o nome {0} já existe", plano.Nome);
            return null;
        }

        _context.Planos.Add(plano);
        await _context.SaveChangesAsync();
        return plano;
    } 
    public async Task<Plano> UpdatePlano(int id, Plano plano)
    {
        var planoExist = await _context.Planos.FindAsync(id);

        if(planoExist == null)
        {
            _logger.LogError("Plano com o id {0} não existe", id);
            return null;
        }

        var nomeExist = await _context.Planos.Where(p => p.Nome == plano.Nome).FirstOrDefaultAsync();

        if (nomeExist == null)
        {
            _logger.LogError("Plano com o nome {0} já existe", plano.Nome);
            return null;
        }

        planoExist.Nome = plano.Nome;
        planoExist.Preco = plano.Preco;
        planoExist.Descricao = plano.Descricao;
        planoExist.DataInicio = plano.DataInicio;
        planoExist.DataFim = plano.DataFim;

        await _context.SaveChangesAsync();
        return planoExist;
    }

    public async Task<bool> DeletePlano(int id)
    {
        var removePlano = await _context.Planos.FindAsync(id);

        if (removePlano == null)
        {
            _logger.LogError("Plano com o id {0} não existe", id);
            return false;
        }

        var alunosAssociados = await _context.Alunos.AnyAsync(a => a.PlanoId == id);
        if (alunosAssociados)
        {
            _logger.LogError("Não é possível deletar o plano com o id {0} porque há alunos associados.", id);
            return false;
        }

        _context.Planos.Remove(removePlano);
        await _context.SaveChangesAsync();

        return true;
    }
}
