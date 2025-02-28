using AutoMapper;
using DevStudy.Application.DTOs.Treino;
using DevStudy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Mappers.Mapping;

public class TreinoMappingProfile : Profile
{
    public TreinoMappingProfile()
    {
        CreateMap<TreinoDTO, Treino>().ReverseMap().ForMember(dest => dest.Exercicios, opt => opt.MapFrom(src => src.Exercicios.Select(te => te.Exercicio))); 
        CreateMap<TreinoCreateDTO, Treino>().ReverseMap();
    }
}
