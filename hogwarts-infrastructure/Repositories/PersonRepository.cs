using hogwarts_core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hogwarts_infrastructure.Repositories
{
    public class PersonRepository
    {
        public IEnumerable<Person> GetPeople() => Enumerable.Range(1, 10).Select(i => new Person()
        {
            Id = i,
            FirstName = $"Nombre {i}",
            LastName = $"Apellido {i}",
            Age = i + 20,
        });
    }
}
