using System;

namespace BigDataOrdersDashboard.Entities;

public class Order
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public int Quantity { get; set; }
    public string PaymentMethod { get; set; } = default!;
    public string OrderStatus { get; set; } = default!;
    public DateTime OrderDate { get; set; }
    public string? OrderNotes { get; set; } = default!;


}
