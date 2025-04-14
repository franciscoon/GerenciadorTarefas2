using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Gerenciador.Application.Interface;
using Gerenciador.Domain.Interface;

namespace Gerenciador.Application.Service
{
    public class BaseService<TDto, TCreateDto, TUpdateDto, TEntity> : IBaseService<TDto, TCreateDto, TUpdateDto> where TEntity : class where TDto : class
    {
        protected readonly IBaseRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception("Objeto não encontrado");
            }

            return _mapper.Map<TDto>(entity);
        }


        public virtual async Task<TDto> CreateAsync(TCreateDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);

            await _repository.CreateAsync(entity);

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task UpdateAsync(int id, TUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception("Objeto não encontrado");
            }

            _mapper.Map(dto, entity);

            await _repository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception("Objeto não encontrado");
            }

            await _repository.DeleteAsync(entity);
        }
    }
}
