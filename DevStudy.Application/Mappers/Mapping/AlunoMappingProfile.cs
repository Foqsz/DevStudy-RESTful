using AutoMapper;
using DevStudy.Application.DTOs.Aluno;
using DevStudy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Mappers.Mapping;

public class AlunoMappingProfile : Profile
{
    public AlunoMappingProfile()
    {
        CreateMap<AlunoDTO, Aluno>().ReverseMap();
        CreateMap<AlunoCreateDTO, Aluno>().ReverseMap();
        CreateMap<AlunoUpdateDTO, Aluno>().ReverseMap();
    } 
}
