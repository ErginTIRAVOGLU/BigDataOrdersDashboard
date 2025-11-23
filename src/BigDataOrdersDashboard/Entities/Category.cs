namespace BigDataOrdersDashboard.Entities;

public class Category
{
    public int CategoryId { get; set; }=default!;
    public string CategoryName { get; set; }=default!;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
