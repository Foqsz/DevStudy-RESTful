using DevStudy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.DTOs.Treino;

public class TreinoDTO
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public List<Exercicio> Exercicios { get; set; } // Lista de exercícios do treino 
}
