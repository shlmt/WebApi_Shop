using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class UserDTO
    {
        [MinLength(2)]
        public string FirstName { get; set; } = null!;

        [MaxLength(10)]
        public string LastName { get; set; } = null!;

        [EmailAddress]
        public string? Email { get; set; }

        public string Password { get; set; } = null!;

    }
}
