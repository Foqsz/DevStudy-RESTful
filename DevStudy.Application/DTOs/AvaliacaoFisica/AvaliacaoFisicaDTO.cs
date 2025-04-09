using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.DTOs.AvaliacaoFisica;

public class AvaliacaoFisicaDTO
{

    public int Id { get; set; }

    public int AlunoId { get; set; } // Relacionamento com Aluno

    public DateTime Data { get; set; }

    public decimal Peso { get; set; }

    public decimal Altura { get; set; } 
}
