using System.ComponentModel.DataAnnotations;

namespace DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

public class TreinoViewModel
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public List<ExercicioViewModel> Exercicios { get; set; } // Lista de exercícios do treino 
}
