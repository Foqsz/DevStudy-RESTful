using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Core.Models;

public class Treino
{
    public int Id { get; set; }
    public int AlunoId { get; set; } // Relacionamento com Aluno
    public DateTime Data { get; set; } // Data do treino
    public List<TreinoExercicio> Exercicios { get; set; } // Relacionamento N:M entre Treino e Exercicio
}
