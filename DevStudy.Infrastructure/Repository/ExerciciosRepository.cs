using DevStudy.Domain.Models;
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

public class ExerciciosRepository : IExerciciosRepository
{
    private readonly DataBaseContext _context;
    private ILogger<ExerciciosRepository> _logger;

    public ExerciciosRepository(DataBaseContext context, ILogger<ExerciciosRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Exercicio>> GetExercicios()
    {
       return await _context.Exercicios.AsNoTracking().ToListAsync();
    }

    public async Task<Exercicio> GetExercicioById(int id)
    {
        var exercicioId = await _context.Exercicios.SingleOrDefaultAsync(e => e.Id == id);

        return exercicioId;
    }

    public async Task<Exercicio> CreateExercicio(Exercicio exercicio)
    {
        var treinoExist = await _context.Exercicios.AnyAsync(e => e.Id == exercicio.Id);
        if (treinoExist)
        {
            throw new InvalidOperationException("Exercicio já existe.");
        }

        await _context.Exercicios.AddAsync(exercicio);
        await _context.SaveChangesAsync();

        return exercicio;
    }

    public async Task<Exercicio> UpdateExercicio(int id, Exercicio exercicio)
    {
        var checkExercicio = await _context.Exercicios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (checkExercicio == null)
        {
            throw new InvalidOperationException("Exercicio não encontrado.");
        }

        _context.Exercicios.Update(exercicio);
        await _context.SaveChangesAsync();
        return exercicio;
    }

    public async Task<bool> DeleteExercicio(int id)
    {
        var exercicioExist = await _context.Exercicios.FindAsync(id);

        if (exercicioExist != null)
        {
            _context.Exercicios.Remove(exercicioExist);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
