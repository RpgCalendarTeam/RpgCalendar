namespace RPGCalendar.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public interface IEntityRepository<TEntity>
        where TEntity : EntityBase
    {
        Task<List<TEntity>> FetchAllAsync();
        Task<TEntity?> FetchByIdAsync(int id);
        Task<TEntity?> InsertAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(int id, TEntity entity);
        Task<bool> DeleteAsync(int id);
    }

    public abstract class EntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : EntityBase
    {
        protected ApplicationDbContext DbContext { get; }

        protected virtual IQueryable<TEntity> Query => DbContext.Set<TEntity>();

        public EntityRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            TEntity entity = await Query.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is { })
            {
                DbContext.Remove(entity);
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public virtual async Task<List<TEntity>> FetchAllAsync()
        {
            return await Query.ToListAsync();
        }

        public virtual async Task<TEntity?> FetchByIdAsync(int id)
        {
            return await Query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity?> InsertAsync(TEntity entity)
        {
            DbContext.Add(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity?> UpdateAsync(int id, TEntity entity)
        {
            if (await Query.FirstOrDefaultAsync(x => x.Id == id) is TEntity result)
            {
                result = entity;
                await DbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
