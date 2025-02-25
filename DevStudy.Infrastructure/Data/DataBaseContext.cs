using DevStudy.Core.Models;
using Microsoft.EntityFrameworkCore; 

namespace DevStudy.Infrastructure.Data;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
    }
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Exercicio> Exercicios { get; set; }
    public DbSet<Treino> Treinos { get; set; }
    public DbSet<TreinoExercicio> TreinoExercicios { get; set; }
    public DbSet<Plano> Planos { get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }
    public DbSet<Instrutor> Instrutores { get; set; }
    public DbSet<AvaliacaoFisica> AvaliacoesFisicas { get; set; }
}
