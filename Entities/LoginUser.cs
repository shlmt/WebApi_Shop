using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class LoginUser
    {
        [EmailAddress,Required]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }
    }
}
