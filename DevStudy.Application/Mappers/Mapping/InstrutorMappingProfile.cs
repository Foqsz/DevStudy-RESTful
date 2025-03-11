using AutoMapper;
using DevStudy.Application.DTOs.Instrutor;
using DevStudy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Mappers.Mapping
{
    public class InstrutorMappingProfile : Profile
    {
        public InstrutorMappingProfile()
        {
            CreateMap<InstrutorDTO, Instrutor>().ReverseMap().ForMember(dest => dest.Alunos, opt => opt.MapFrom(src => src.Alunos)); // Mapeando Alunos para AlunoDTO;
        }
    }
}
