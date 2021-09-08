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

        public async Task<bool> InsertPerson(Person person)
        {
            hogwartsContext.People.Add(person);
            var result = await hogwartsContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdatePerson(Person person)
        {
            var currentPerson = await GetPerson(person.PersonId);
            if (currentPerson == null) return false;

            currentPerson.FirstName = person.FirstName;
            currentPerson.LastName = person.LastName;
            currentPerson.Age = person.Age;

            int rows = await hogwartsContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeletePerson(string id)
        {
            var currentPerson = await GetPerson(id);

            hogwartsContext.People.Remove(currentPerson);

            int rows = await hogwartsContext.SaveChangesAsync();
            return rows > 0;
        }
    }
}
