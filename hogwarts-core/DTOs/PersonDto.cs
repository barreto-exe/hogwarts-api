using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace hogwarts_core.DTOs
{
    public partial class PersonDto
    {
        [Required]
        [StringLength(10)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Sólo se aceptan dígitos.")]
        public string PersonId { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [Range(18, 99)]
        public int Age { get; set; }
    }
}
