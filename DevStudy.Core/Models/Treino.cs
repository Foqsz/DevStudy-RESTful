using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Core.Models;

public class Treino
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int AlunoId { get; set; } // Relacionamento com Aluno

    [Required]
    public DateTime Data { get; set; } // Data do treino

    [Required]
    public List<TreinoExercicio> Exercicios { get; set; } // Relacionamento N:M entre Treino e Exercicio
}
