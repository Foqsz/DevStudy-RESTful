using System.ComponentModel.DataAnnotations;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

public class InstrutorViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; }

    [Required]
    [StringLength(50)]
    public string Especialidade { get; set; } // Ex: "Musculação", "Yoga"

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string Telefone { get; set; }

    public List<AlunoViewModel>? Alunos { get; set; } // Relacionamento N:M entre Instrutor e Aluno (caso o instrutor tenha múltiplos alunos)
}
