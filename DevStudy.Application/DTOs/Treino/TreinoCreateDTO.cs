using DevStudy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.DTOs.Treino;

public class TreinoCreateDTO
{
    public int AlunoId { get; set; }    
    public DateTime Data { get; set; }
    public int ExercicioId { get; set; } = new();
}
