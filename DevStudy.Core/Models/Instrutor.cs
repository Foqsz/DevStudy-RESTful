using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Core.Models;

public class Instrutor
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Especialidade { get; set; } // Ex: "Musculação", "Yoga"
    public string Email { get; set; }
    public string Telefone { get; set; }
    public List<Aluno> Alunos { get; set; } // Relacionamento N:M entre Instrutor e Aluno (caso o instrutor tenha múltiplos alunos)
}
