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
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly HogwartsContext hogwartsContext;
        private readonly DbSet<T> entities;
        public BaseRepository(HogwartsContext hogwartsContext)
        {
            this.hogwartsContext = hogwartsContext;
            this.entities = hogwartsContext.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await entities.FindAsync(id);
        }

        public async Task Add(T item)
        {
            await entities.AddAsync(item);
        }

        public void Update(T item)
        {
            entities.Update(item);
        }

        public async Task Delete(object id)
        {
            var currentEntity = await GetById(id);
            entities.Remove(currentEntity);
        }
    }
}
