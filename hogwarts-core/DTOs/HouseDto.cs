using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace hogwarts_core.DTOs
{
    public partial class HouseDto
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
