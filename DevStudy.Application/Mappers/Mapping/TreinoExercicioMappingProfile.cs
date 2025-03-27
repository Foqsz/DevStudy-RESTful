using AutoMapper;
using DevStudy.Application.DTOs.TreinoExercicio;
using DevStudy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStudy.Application.Mappers.Mapping
{
    public class TreinoExercicioMappingProfile : Profile
    {
        public TreinoExercicioMappingProfile()
        {
            CreateMap<TreinoExercicioCreateDTO, TreinoExercicio>().ReverseMap();
            CreateMap<TreinoExercicioDTO, TreinoExercicio>().ReverseMap();
        }
    }
}
