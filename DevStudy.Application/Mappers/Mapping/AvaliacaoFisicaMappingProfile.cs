using AutoMapper;
using DevStudy.Application.DTOs.AvaliacaoFisica;
using DevStudy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Mappers.Mapping;

public class AvaliacaoFisicaMappingProfile : Profile
{
    public AvaliacaoFisicaMappingProfile()
    {
        CreateMap<AvaliacaoFisicaDTO, AvaliacaoFisica>().ReverseMap();
    }
}
