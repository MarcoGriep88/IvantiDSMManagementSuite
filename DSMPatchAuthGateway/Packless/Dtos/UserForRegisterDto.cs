using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Packless.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(45, MinimumLength=8, ErrorMessage="You must specify password between 8 and 45 characters")]
        public string Password { get; set; }
    }
}
