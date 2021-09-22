using hogwarts_core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hogwarts_core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(object id);
        Task Add(T item);
        void Update(T item);
        Task Delete(object id);
    }
}
