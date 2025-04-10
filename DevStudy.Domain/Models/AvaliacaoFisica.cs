﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Domain.Models;

public class AvaliacaoFisica
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int AlunoId { get; set; } // Relacionamento com Aluno

    [Required]
    public DateTime Data { get; set; }

    [Required]
    [Range(0, 300)]
    public decimal Peso { get; set; }

    [Required]
    [Range(0, 3)]
    public decimal Altura { get; set; }

    [Required]
    [Range(0, 100)]
    public decimal IMC { get; set; } // Cálculo do Índice de Massa Corporal

    [Required]
    [Range(0, 100)]
    public decimal PercentualGordura { get; set; }
    public string ClassificacaoIMC { get; set; } // Classificação do IMC (Abaixo do peso, Peso normal, Sobrepeso, Obesidade I, II ou III)
}
