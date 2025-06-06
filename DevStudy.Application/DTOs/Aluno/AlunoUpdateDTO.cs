﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.DTOs.Aluno;

public class AlunoUpdateDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public int PlanoId { get; set; }
    public int InstrutorId { get; set; }
    public bool Ativo { get; set; }
}
