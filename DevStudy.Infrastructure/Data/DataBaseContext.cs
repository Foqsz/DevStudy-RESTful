using DevStudy.Domain.Models;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração do modelo "Plano"
        modelBuilder.Entity<Plano>(entity =>
        {
            // Chave primária
            entity.HasKey(p => p.Id);

            // Configuração da propriedade "Nome"
            entity.Property(p => p.Nome)
                .IsRequired() // Torna o nome obrigatório
                .HasMaxLength(100); // Define o comprimento máximo do nome

            // Configuração da propriedade "Preco"
            entity.Property(p => p.Preco)
                .HasColumnType("decimal(18,2)") // Define o tipo como decimal com 2 casas decimais
                .IsRequired(); // Torna o preço obrigatório

            // Configuração da propriedade "Descricao"
            entity.Property(p => p.Descricao)
                .HasMaxLength(500); // Define o comprimento máximo da descrição

            // Configuração da propriedade "DataInicio"
            entity.Property(p => p.DataInicio)
                .IsRequired(); // Torna a duração obrigatória

            // Configuração da propriedade "DataFim"
            entity.Property(p => p.DataFim)
                .IsRequired(); // Torna a duração obrigatória
        });

        modelBuilder.Entity<TreinoExercicio>()
            .HasOne(te => te.Aluno)
            .WithMany(a => a.Treinos)
            .HasForeignKey(te => te.AlunoId); // Alterado para AlunoId

        modelBuilder.Entity<Aluno>()
            .HasOne(a => a.Instrutor)
            .WithMany(i => i.Alunos)
            .HasForeignKey(a => a.InstrutorId);

        modelBuilder.Entity<Aluno>()
            .HasOne(a => a.Plano)
            .WithMany() // Sem coleção em Plano, pois é 1:1
            .HasForeignKey(a => a.PlanoId)
            .IsRequired();
    }
}