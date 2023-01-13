namespace OrderSvc.Entity;

public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }

    public string ProductName { get; set; }
    public decimal Price { get; set; }

    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string Email { get; set; }
}