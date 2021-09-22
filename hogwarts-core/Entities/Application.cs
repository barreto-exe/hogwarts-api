using System;
using System.Collections.Generic;

#nullable disable

namespace hogwarts_core.Entities
{
    public partial class Application : BaseEntity
    {
        public int ApplicationId { get; set; }
        public string PersonId { get; set; }
        public string AspiredHouse { get; set; }

        public virtual House AspiredHouseNavigation { get; set; }
        public virtual Person Person { get; set; }
    }
}
