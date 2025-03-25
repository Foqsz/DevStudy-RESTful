using System.ComponentModel.DataAnnotations;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

public class AlunoViewModel
{ 
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Senha { get; set; } // Pode ser criptografada

    [Required]
    public DateTime DataNascimento { get; set; }

    [Required]
    public DateTime DataInscricao { get; set; }

    [Required]
    public int PlanoId { get; set; } // Ex: "Mensal", "Anual"
    public PlanoViewModel Plano { get; set; } // Relacionamento 1:1 entre Aluno e Plano

    public bool Ativo { get; set; } // Verifica se o aluno está ativo na academia
    public int InstrutorId { get; set; } // Relacionamento com Instrutor
    public InstrutorViewModel Instrutor { get; set; } // Relacionamento 1:M entre Aluno e Instrutor

    public List<TreinoViewModel> Treinos { get; set; } // Relacionamento com Treinos
}
