using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace hogwarts_core.DTOs
{
    public partial class ApplicationDto
    {
        public int ApplicationId { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Sólo se aceptan dígitos.")]
        public string PersonId { get; set; }

        [Required]
        [StringLength(20)]
        public string AspiredHouse { get; set; }
    }
}
