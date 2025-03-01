using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Domain.Models;

public class TreinoExercicio
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int TreinoId { get; set; } // Relacionamento com Treino

    [Required]
    public int ExercicioId { get; set; } // Relacionamento com Exercicio

    [Range(1, int.MaxValue, ErrorMessage = "Repeticoes must be greater than 0")]
    public int Repeticoes { get; set; } // Quantidade de repetições do exercício

    [Range(1, int.MaxValue, ErrorMessage = "Series must be greater than 0")]
    public int Series { get; set; } // Quantidade de séries
    public Exercicio Exercicio { get; set; }
}
