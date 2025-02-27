using DevStudy.Core.Models;
using DevStudy.Domain.Interfaces;
using DevStudy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Infrastructure.Repository
{
    public class TreinosRepository : ITreinosRepository
    {
        private readonly DataBaseContext _context;
        private ILogger<TreinosRepository> _logger;

        public TreinosRepository(DataBaseContext context, ILogger<TreinosRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Treino>> GetTreinos()
        {
            return await _context.Treinos.Include(t => t.Aluno).ToListAsync();
        }

        public async Task<Treino> GetTreinoById(int id)
        {
            return await _context.Treinos.Include(t => t.Aluno).Include(t => t.Exercicios).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Treino> CreateTreino(Treino treino)
        {
            var aluno = await _context.Alunos.FindAsync(treino.AlunoId);
            if (aluno == null)
            {
                _logger.LogError("Aluno não encontrado");
                return null;
            }

            _context.Treinos.Add(treino);
            await _context.SaveChangesAsync();

            return treino;
        }

        public async Task<Treino> UpdateTreino(int id, Treino treino)
        {
            var aluno = _context.Alunos.Find(treino.AlunoId);

            if (aluno == null)
            {
                _logger.LogError("Aluno não encontrado");
            }

            _context.Treinos.Update(treino);
            await _context.SaveChangesAsync();

            return treino;
        }

        public async Task<bool> DeleteTreino(int id)
        {
            var treino = await _context.Treinos.FindAsync(id);

            if(treino == null)
            {
                _logger.LogError("Treino não encontrado");
                return false;
            }

            _context.Treinos.Remove(treino);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
