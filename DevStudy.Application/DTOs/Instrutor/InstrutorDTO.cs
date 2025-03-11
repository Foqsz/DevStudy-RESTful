using DevStudy.Application.DTOs.Aluno;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.DTOs.Instrutor
{
    public class InstrutorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Especialidade { get; set; }  

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Telefone { get; set; }
        public List<AlunoDTO> Alunos { get; set; }
    }
}
