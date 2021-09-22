using hogwarts_core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hogwarts_core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll(object id);
        Task<T> GetById(object id);
        Task<bool> Add(T item);
        Task<bool> Update(T item);
        Task<bool> Delete(object id);
    }
}
