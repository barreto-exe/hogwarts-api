using hogwarts_core.Entities;
using hogwarts_core.Interfaces;
using hogwarts_infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hogwarts_infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly HogwartsContext hogwartsContext;
        private readonly DbSet<T> entities;
        protected BaseRepository(HogwartsContext hogwartsContext)
        {
            this.hogwartsContext = hogwartsContext;
            this.entities = hogwartsContext.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAll(object id)
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<bool> Add(T item)
        {
            entities.Add(item);
            int result = await hogwartsContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> Update(T item)
        {
            entities.Update(item);
            int result = await hogwartsContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> Delete(object id)
        {
            var currentEntity = await GetById(id);
            if (currentEntity == null) return false;

            entities.Remove(currentEntity);

            int rows = await hogwartsContext.SaveChangesAsync();
            return rows > 0;
        }
    }
}
