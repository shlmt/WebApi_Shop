using System;
using System.Collections.Generic;

namespace DTOs;

public partial class OrderItemDTO
{

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual ProductDTO Product { get; set; } = null!;
}
