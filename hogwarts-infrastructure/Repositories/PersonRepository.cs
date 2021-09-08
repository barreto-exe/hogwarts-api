using hogwarts_core.Entities;
using hogwarts_core.Interfaces;
using hogwarts_infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hogwarts_infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly HogwartsContext hogwartsContext;

        public PersonRepository(HogwartsContext hogwartsContext)
        {
            this.hogwartsContext = hogwartsContext;
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            return await hogwartsContext.People.ToListAsync();
        }

        public async Task<Person> GetPerson(string id)
        {
            return await hogwartsContext.People.FirstOrDefaultAsync(x => x.PersonId == id);
        }

        public async Task InsertPerson(Person person)
        {
            hogwartsContext.People.Add(person);
            await hogwartsContext.SaveChangesAsync();
        }
    }
}
