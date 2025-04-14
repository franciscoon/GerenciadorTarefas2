using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Gerenciador.Application.Dto;
using Gerenciador.Domain.Entities;

namespace Gerenciador.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Tarefa, TarefaDto>().ReverseMap();
            CreateMap<Tarefa, CreateTarefaDto>().ReverseMap();
            CreateMap<Tarefa, UpdateTarefaDto>().ReverseMap();
        }
    }
}
