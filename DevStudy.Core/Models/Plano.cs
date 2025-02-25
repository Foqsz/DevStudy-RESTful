using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Core.Models;

public class Plano
{
    public int Id { get; set; }
    public string Nome { get; set; } // Ex: "Mensal", "Trimestral"
    public decimal Preco { get; set; }
    public string Descricao { get; set; }
    public int DuracaoMeses { get; set; } // Duração do plano em meses
}
