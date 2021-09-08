using hogwarts_core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hogwarts_core.Interfaces
{
    public interface IPersonRepository
    {
        public Task<IEnumerable<Person>> GetPeople();
        public Task<Person> GetPerson(string id);
        public Task<bool> InsertPerson(Person person);
        public Task<bool> UpdatePerson(Person person);
        public Task<bool> DeletePerson(string id);
    }
}
