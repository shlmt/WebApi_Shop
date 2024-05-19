using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DTOs;

public partial class OrderDTO
{

    public DateOnly OrderDate { get; set; }

    public int OrderSum { get; set; }

    [JsonIgnore]
    public virtual ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();

}
