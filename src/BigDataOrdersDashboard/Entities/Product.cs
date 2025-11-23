using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BigDataOrdersDashboard.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }= default!;
    public string ProductDescription { get; set; }= default!;
    public double UnitPrice { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
    [ValidateNever]
    public virtual Category Category { get; set; }= default!;
    public string CountryOfOrigin { get; set; }= default!;
    public string ProductImageUrl { get; set; }= default!;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

}
 
