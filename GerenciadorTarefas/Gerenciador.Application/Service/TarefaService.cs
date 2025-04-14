using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Gerenciador.Application.Dto;
using Gerenciador.Application.Interface;
using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interface;

namespace Gerenciador.Application.Service
{
    public class TarefaService : BaseService<TarefaDto, CreateTarefaDto, UpdateTarefaDto, Tarefa>, ITarefaService
    {
        public TarefaService(IBaseRepository<Tarefa> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public override async Task<TarefaDto> CreateAsync(CreateTarefaDto dto)
        {
            if (dto.DataConclusao.HasValue && dto.DataConclusao < DateTime.Now)
            {
                throw new ArgumentException("A data de conclusão não pode ser anterior à data de criação.");
            }

            if (dto.Titulo.Length > 100)
            {
                throw new ArgumentException("O título deve ter no máximo 100 caracteres");
            }

            return await base.CreateAsync(dto);
        }

        public override async Task UpdateAsync(int id, UpdateTarefaDto dto)
        {
            if (dto.DataConclusao.HasValue && dto.DataConclusao < DateTime.Now)
            {
                throw new ArgumentException("A data de conclusão não pode ser anterior à data de criação.");
            }

            if (dto.Titulo.Length > 100)
            {
                throw new ArgumentException("O título deve ter no máximo 100 caracteres");
            }

            await base.UpdateAsync(id, dto);
        }

    }
}
