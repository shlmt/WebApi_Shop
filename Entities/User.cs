using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    [NotMapped]
    public string? Token { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
