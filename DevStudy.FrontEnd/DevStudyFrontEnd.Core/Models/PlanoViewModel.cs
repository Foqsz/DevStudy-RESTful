namespace DevStudy.FrontEnd.DevStudyFrontEnd.Core.Models;

public class PlanoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; } // Ex: "Mensal", "Trimestral"
    public decimal Preco { get; set; }
    public string Descricao { get; set; }
    public DateTime DataInicio { get; set; } // Data de início do plano
    public DateTime DataFim { get; set; } // Data de término do plano
}
