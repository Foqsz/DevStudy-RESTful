using System.ComponentModel.DataAnnotations;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

public class TreinoViewModel
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public DateTime Data { get; set; }
    public int ExercicioId { get; set; } = new();
}
