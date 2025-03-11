using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace DevStudy.Domain.Models;

public class Instrutor
{
    [Key]
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

    public List<Aluno> Alunos { get; set; } // Relacionamento N:M entre Instrutor e Aluno (caso o instrutor tenha múltiplos alunos)
}
