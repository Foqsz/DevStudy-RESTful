using DevStudy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.DTOs.Aluno;

public class AlunoDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Plano { get; set; }
    public bool Ativo { get; set; }
    public List<Treino> Treinos { get; set; } // Relacionamento com Treinos
}
