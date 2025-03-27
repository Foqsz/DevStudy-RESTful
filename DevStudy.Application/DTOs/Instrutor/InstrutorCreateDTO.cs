using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.DTOs.Instrutor;

public class InstrutorCreateDTO
{ 
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; }

    [Required]
    [StringLength(50)]
    public string Especialidade { get; set; } // Ex: "Musculação", "Yoga"

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string Telefone { get; set; }
}
