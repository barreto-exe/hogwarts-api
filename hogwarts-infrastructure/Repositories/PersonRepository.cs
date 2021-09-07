using hogwarts_core.Entities;
using hogwarts_core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hogwarts_infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public async Task<IEnumerable<Person>> GetPeople()
        {
            var ppl = Enumerable.Range(1, 10).Select(i => new Person()
            {
                Id = i,
                FirstName = $"Nombre {i}",
                LastName = $"Apellido {i}",
                Age = i + 20,
            });

            await Task.Delay(10);

            return ppl;
        }
    }
}
