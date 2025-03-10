using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.DTOs.Aluno;

public class AlunoCreateDTO
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }  
    public DateTime DataNascimento { get; set; }
    public bool Ativo { get; set; }
    public int InstrutorId { get; set; }
    public string Plano { get; set; }
}
