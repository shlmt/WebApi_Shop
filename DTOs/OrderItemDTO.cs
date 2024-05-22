using System;
using System.Collections.Generic;

namespace DTOs;

public partial class OrderItemDTO
{
    public int OrderItemId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

}
