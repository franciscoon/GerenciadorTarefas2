using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gerenciador.Application.Interface;
using Gerenciador.Domain.Enuns;

namespace Gerenciador.Application.Dto
{
    public class CreateTarefaDto
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        public string Titulo {  get; set; }
        public string Descricao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public EStatus Status { get; set; }
    }
}
