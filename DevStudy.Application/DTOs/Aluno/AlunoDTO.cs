using DevStudy.Application.DTOs.Instrutor;
using DevStudy.Application.DTOs.Treino;
using DevStudy.Domain.Models; 
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
    public Plano Plano { get; set; }
    public bool Ativo { get; set; }
    public InstrutorDTO Instrutor { get; set; } // Relacionamento N:M entre Aluno e Instrutor
    public List<TreinoDTO> Treinos { get; set; } // Relacionamento com Treinos
}
