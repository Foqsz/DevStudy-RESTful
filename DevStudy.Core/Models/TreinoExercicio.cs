using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Core.Models;

public class TreinoExercicio
{
    public int Id { get; set; }
    public int TreinoId { get; set; } // Relacionamento com Treino
    public int ExercicioId { get; set; } // Relacionamento com Exercicio
    public int Repeticoes { get; set; } // Quantidade de repetições do exercício
    public int Series { get; set; } // Quantidade de séries
}
