// ReSharper disable UnusedMember.Global
namespace OrderSvc.Entity;

public class CustomerOrderHistory
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public decimal Price { get; set; }
    public string CustomerName { get; set; }
}