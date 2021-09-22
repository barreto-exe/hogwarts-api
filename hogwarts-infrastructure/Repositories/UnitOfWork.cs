using hogwarts_core.Entities;
using hogwarts_core.Interfaces;
using hogwarts_infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hogwarts_infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HogwartsContext hogwartsContext;
        private readonly IRepository<Application> applicationRepository;
        private readonly IRepository<Person> personRepository;
        private readonly IRepository<House> houseRepository;
        public UnitOfWork(HogwartsContext hogwartsContext)
        {
            this.hogwartsContext = hogwartsContext;
        }


        public IRepository<Application> ApplicationRepository => applicationRepository ?? new BaseRepository<Application>(hogwartsContext);

        public IRepository<House> HouseRepository => houseRepository ?? new BaseRepository<House>(hogwartsContext);

        public IRepository<Person> PersonRepository => personRepository ?? new BaseRepository<Person>(hogwartsContext);

        public void Dispose()
        {
            if (hogwartsContext != null)
            {
                hogwartsContext.Dispose();
            }
        }

        public void SaveChanges()
        {
            hogwartsContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await hogwartsContext.SaveChangesAsync();
        }
    }
}
