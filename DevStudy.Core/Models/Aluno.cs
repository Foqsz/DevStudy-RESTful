using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Core.Models;

public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; } // Pode ser criptografada
    public DateTime DataNascimento { get; set; }
    public DateTime DataInscricao { get; set; }
    public string Plano { get; set; } // Ex: "Mensal", "Anual"
    public bool Ativo { get; set; } // Verifica se o aluno está ativo na academia
    public List<Treino> Treinos { get; set; } // Relacionamento com Treinos
}
