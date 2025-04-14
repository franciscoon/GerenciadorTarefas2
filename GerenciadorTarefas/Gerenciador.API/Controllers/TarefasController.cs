using AutoMapper;
using Gerenciador.Application.Dto;
using Gerenciador.Application.Interface;
using Gerenciador.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : BaseController<TarefaDto, CreateTarefaDto, UpdateTarefaDto>
    {
        public TarefasController(ITarefaService service) : base(service)
        {
        }
    }
}
