namespace DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

public class ExercicioViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; } // Descrição do exercício
    public string Tipo { get; set; } // Ex: "Cardio", "Força"
    public int DuraçãoMinutos { get; set; } // Duração recomendada do exercício
}
