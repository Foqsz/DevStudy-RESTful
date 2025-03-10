using DevStudy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Domain.Interfaces;

public interface IInstrutorRepository
{
    Task<IEnumerable<Instrutor>> GetInstrutores();
    Task<Instrutor> GetInstrutor(int id);
    Task<Instrutor> GetInstrutorByEmail(string email);
    Task<Instrutor> CreateInstrutor(Instrutor instrutor);
    Task<Instrutor> UpdateInstrutor(int id, Instrutor instrutor);
    Task<bool> DeleteInstrutor(int id);
}
