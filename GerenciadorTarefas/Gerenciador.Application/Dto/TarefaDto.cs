using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gerenciador.Application.Interface;
using Gerenciador.Domain.Enuns;

namespace Gerenciador.Application.Dto
{
    public class TarefaDto : IHasId
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataConclusao { get; set; }
        public EStatus Status { get; set; }
    }
}
