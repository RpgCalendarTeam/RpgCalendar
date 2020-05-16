using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RPGCalendar.Data;

namespace RPGCalendar.Core.Services
{
    using Repositories;

    public interface IEntityService<TDto, TInputDto>
        where TInputDto : class
        where TDto : class, TInputDto
    {
        Task<List<TDto>> FetchAllAsync();
        Task<TDto?> FetchByIdAsync(int id);
        Task<TDto?> InsertAsync(TInputDto entity);
        Task<TDto?> UpdateAsync(int id, TInputDto input);
        Task<bool> DeleteAsync(int id);
    }

    public abstract class EntityService<TDto, TInputDto, TEntity, TEntityRepository> : IEntityService<TDto, TInputDto>
        where TEntityRepository : IEntityRepository<TEntity>
        where TEntity : EntityBase
        where TDto : class, TInputDto
        where TInputDto : class
    {
        protected TEntityRepository EntityRepository { get; }
        protected IMapper Mapper { get; }

        public EntityService(IMapper mapper, TEntityRepository entityRepository)
        {
            EntityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public virtual Task<bool> DeleteAsync(int id)
            => EntityRepository.DeleteAsync(id);

        public virtual async Task<List<TDto>> FetchAllAsync()
        {
            return Mapper.Map<List<TEntity>, List<TDto>>(await EntityRepository.FetchAllAsync());
        }

        public virtual async Task<TDto?> FetchByIdAsync(int id)
        {
            TEntity? entity = await EntityRepository.FetchByIdAsync(id);
            return entity is null 
                ? null 
                : Mapper.Map<TEntity, TDto>(entity);
        }

        public virtual async Task<TDto?> InsertAsync(TInputDto dto)
        {
            TEntity entity = Mapper.Map<TInputDto, TEntity>(dto);
            await EntityRepository.InsertAsync(entity);
            return Mapper.Map<TEntity, TDto>(entity);
        }

        public virtual async Task<TDto?> UpdateAsync(int id, TInputDto input)
        {
            TEntity entity = Mapper.Map<TInputDto, TEntity>(input);
            TEntity? result = await EntityRepository.UpdateAsync(id, entity);
            return entity is null
                ? null
                : Mapper.Map<TEntity, TDto>(entity);
        }
    }
}
