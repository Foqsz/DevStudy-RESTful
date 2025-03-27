using DevStudy.Application.DTOs.Instrutor;
using DevStudy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Interfaces;

public interface IInstrutorService
{
    Task<IEnumerable<InstrutorDTO>> GetInstrutores();
    Task<InstrutorDTO> GetInstrutor(int id);
    Task<InstrutorDTO> GetInstrutorByEmail(string email);
    Task<InstrutorCreateDTO> CreateInstrutor(InstrutorCreateDTO instrutor);
    Task<InstrutorCreateDTO> UpdateInstrutor(int id, InstrutorCreateDTO instrutor);
    Task<bool> DeleteInstrutor(int id); 
}
