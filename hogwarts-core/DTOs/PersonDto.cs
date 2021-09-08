using System;
using System.Collections.Generic;

#nullable disable

namespace hogwarts_core.DTOs
{
    public partial class PersonDto
    {
        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
