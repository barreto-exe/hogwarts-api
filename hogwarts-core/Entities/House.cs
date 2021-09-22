using System;
using System.Collections.Generic;

#nullable disable

namespace hogwarts_core.Entities
{
    public partial class House : BaseEntity
    {
        public House()
        {
            Applications = new HashSet<Application>();
        }

        public string Name { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
    }
}
