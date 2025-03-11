using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Domain.Models;

public class Plano
{
    public int Id { get; set; }
    public string Nome { get; set; } // Ex: "Mensal", "Trimestral"
    public decimal Preco { get; set; }
    public string Descricao { get; set; }
    public DateTime DataInicio { get; set; } // Data de início do plano
    public DateTime DataFim { get; set; } // Data de término do plano
}
