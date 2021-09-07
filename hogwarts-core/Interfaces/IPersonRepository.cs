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
    }
}
