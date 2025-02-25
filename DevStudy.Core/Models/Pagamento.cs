using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Core.Models;

public class Pagamento
{
    public int Id { get; set; }
    public int AlunoId { get; set; } // Relacionamento com Aluno
    public decimal Valor { get; set; }
    public DateTime DataPagamento { get; set; }
    public DateTime DataVencimento { get; set; }
    public string FormaPagamento { get; set; } // Ex: "Cartão de Crédito", "Boleto", etc.
    public string Status { get; set; } // Ex: "Pendente", "Pago"
}
