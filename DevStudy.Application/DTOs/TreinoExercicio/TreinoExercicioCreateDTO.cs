using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.DTOs.TreinoExercicio;

public class TreinoExercicioCreateDTO
{
    public int Id { get; set; }

    public int TreinoId { get; set; }

    public int ExercicioId { get; set; }

    public int Repeticoes { get; set; }

    public int Series { get; set; }
}
