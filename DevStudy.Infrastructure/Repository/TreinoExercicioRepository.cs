﻿using DevStudy.Core.Models;
using DevStudy.Domain.Interfaces;
using DevStudy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Infrastructure.Repository;

public class TreinoExercicioRepository : ITreinoExercicioRepository
{
    private readonly DataBaseContext _context;
    private ILogger<TreinoExercicioRepository> _logger;

    public TreinoExercicioRepository(DataBaseContext context, ILogger<TreinoExercicioRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TreinoExercicio>> GetTreinoExercicios()
    {
        return await _context.TreinoExercicios
            .Include(te => te.Exercicio) // Carrega os detalhes do exercício
            .ToListAsync();
    }


    public async Task<TreinoExercicio> GetTreinoExercicioById(int id)
    {
        return await _context.TreinoExercicios.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TreinoExercicio> CreateTreinoExercicio(TreinoExercicio treinoExercicio)
    {
        var treinoExist = await _context.Treinos.AnyAsync(x => x.Id == treinoExercicio.TreinoId);

        if (treinoExist)
        {
            var exercicioExist = await _context.Exercicios.AnyAsync(x => x.Id == treinoExercicio.ExercicioId);
            if (exercicioExist)
            {
                _context.TreinoExercicios.Add(treinoExercicio);
                _context.SaveChanges();
                _logger.LogInformation("TreinoExercicio criado com sucesso");
            }
            else
            {
                _logger.LogError("Exercicio não encontrado");
                return null;
            }
        }
        return null;
    }

    public async Task<TreinoExercicio> UpdateTreinoExercicio(int id, TreinoExercicio treinoExercicio)
    {
        var treinoExist = await _context.Treinos.AnyAsync(x => x.Id == treinoExercicio.TreinoId);

        if (treinoExist)
        {
            var exercicioExist = await _context.Exercicios.AnyAsync(x => x.Id == treinoExercicio.ExercicioId);
            if (exercicioExist)
            {
                _context.TreinoExercicios.Update(treinoExercicio);
                _context.SaveChanges();
                _logger.LogInformation("TreinoExercicio atualizado com sucesso");
            }
            else
            {
                _logger.LogError("Exercicio não encontrado");
                return null;
            }
        }
        return treinoExercicio;
    }

    public async Task<bool> DeleteTreinoExercicio(int id)
    {
        var treinoExist = await _context.TreinoExercicios.FindAsync(id);

        if (treinoExist == null)
        {
            _logger.LogError("TreinoExercicio não encontrado");
            return false;
        }
        _context.Remove(treinoExist);
        await _context.SaveChangesAsync();
        return true;
    }
}
