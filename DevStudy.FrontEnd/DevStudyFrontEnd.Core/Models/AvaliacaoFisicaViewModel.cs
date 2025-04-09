using System.ComponentModel.DataAnnotations;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

public class AvaliacaoFisicaViewModel
{
    public int Id { get; set; }

    [Required]
    public int AlunoId { get; set; } // Relacionamento com Aluno

    [Required]
    public DateTime Data { get; set; }

    [Required]
    [Range(0, 300)]
    public decimal Peso { get; set; }

    [Required] 
    public decimal Altura { get; set; }

    public decimal? IMC { get; set; } // Cálculo do Índice de Massa Corporal
      
    public decimal? PercentualGordura { get; set; }
    public string? ClassificacaoIMC { get; set; }  
}
