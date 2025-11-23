using System;

namespace BigDataOrdersDashboard.Entities;

public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }= default!;
    public string CustomerSurname { get; set; }= default!;
    public string CustomerEmail { get; set; }= default!;
    public string CustomerPhone { get; set; }= default!;
    public string CustomerImageUrl { get; set; }= default!;
    public string CustomerCountry { get; set; }= default!;
    public string CustomerCity { get; set; }= default!;
    public string CustomerDistrict { get; set; }= default!;
    public string CustomerAddress { get; set; }= default!;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
