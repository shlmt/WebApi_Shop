using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DTOs;

public partial class OrderDTO
{

    public DateTime OrderDate { get; set; }

    public int OrderSum { get; set; }
    public int UserId { get; set; }

    public virtual ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();

}
