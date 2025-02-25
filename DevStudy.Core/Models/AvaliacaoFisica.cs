using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Core.Models;

public class AvaliacaoFisica
{
    public int Id { get; set; }
    public int AlunoId { get; set; } // Relacionamento com Aluno
    public DateTime Data { get; set; }
    public decimal Peso { get; set; }
    public decimal Altura { get; set; }
    public decimal IMC { get; set; } // Cálculo do Índice de Massa Corporal
    public decimal PercentualGordura { get; set; }
}
