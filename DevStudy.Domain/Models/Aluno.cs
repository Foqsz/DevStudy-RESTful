using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Domain.Models;

public class Aluno
{
    [Key]
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
    [StringLength(20)]
    public string Plano { get; set; } // Ex: "Mensal", "Anual"

    public bool Ativo { get; set; } // Verifica se o aluno está ativo na academia

    public List<Treino> Treinos { get; set; } // Relacionamento com Treinos
}
