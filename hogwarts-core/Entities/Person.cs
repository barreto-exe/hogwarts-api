using System;
using System.Collections.Generic;

#nullable disable

namespace hogwarts_core.Entities
{
    public partial class Person : BaseEntity
    {
        public Person()
        {
            Applications = new HashSet<Application>();
        }

        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
    }
}
