using Gerenciador.Application.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TDto, TCreateDto, TUpdateDto> : ControllerBase where TDto : IHasId
    {
        protected readonly IBaseService<TDto, TCreateDto, TUpdateDto> _service;

        protected BaseController(IBaseService<TDto, TCreateDto, TUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]TCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task <IActionResult> Update(int id, [FromBody]TUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
