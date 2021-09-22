using hogwarts_core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hogwarts_core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Application> ApplicationRepository { get; }
        IRepository<House> HouseRepository { get; }
        IRepository<Person> PersonRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
