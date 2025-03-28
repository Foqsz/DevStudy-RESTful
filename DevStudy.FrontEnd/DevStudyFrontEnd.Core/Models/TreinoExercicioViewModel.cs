using System.ComponentModel.DataAnnotations;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

public class TreinoExercicioViewModel
{
    public int Id { get; set; }

    [Required]
    public int TreinoId { get; set; } // Relacionamento com Treino

    [Required]
    public int ExercicioId { get; set; } // Relacionamento com Exercicio
    public int AlunoId { get; set; } // Chave estrangeira para Aluno

    [Range(1, int.MaxValue, ErrorMessage = "Repeticoes must be greater than 0")]
    public int Repeticoes { get; set; } // Quantidade de repetições do exercício

    [Range(1, int.MaxValue, ErrorMessage = "Series must be greater than 0")]
    public int Series { get; set; } // Quantidade de séries
    public ExercicioViewModel Exercicio { get; set; }
    public AlunoViewModel Aluno { get; set; }
}
