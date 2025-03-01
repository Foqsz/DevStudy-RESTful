using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Domain.Models;

public class Pagamento
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int AlunoId { get; set; } // Relacionamento com Aluno

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal Valor { get; set; }

    [Required]
    public DateTime DataPagamento { get; set; }

    [Required]
    public DateTime DataVencimento { get; set; }

    [Required]
    [StringLength(50)]
    public string FormaPagamento { get; set; } // Ex: "Cartão de Crédito", "Boleto", etc.

    [Required]
    [StringLength(20)]
    public string Status { get; set; } // Ex: "Pendente", "Pago"
}
