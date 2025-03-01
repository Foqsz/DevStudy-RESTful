using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Domain.Models;

public class Exercicio
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; } // Descrição do exercício
    public string Tipo { get; set; } // Ex: "Cardio", "Força"
    public int DuraçãoMinutos { get; set; } // Duração recomendada do exercício
}
