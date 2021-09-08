using System;
using System.Collections.Generic;

#nullable disable

namespace hogwarts_core.DTOs
{
    public partial class ApplicationDto
    {
        public int ApplicationId { get; set; }
        public string PersonId { get; set; }
        public string AspiredHouse { get; set; }
    }
}
