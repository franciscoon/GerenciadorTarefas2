using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gerenciador.Application.Dto;

namespace Gerenciador.Application.Interface
{
    public interface ITarefaService : IBaseService<TarefaDto, CreateTarefaDto, UpdateTarefaDto>
    {

    }
}
