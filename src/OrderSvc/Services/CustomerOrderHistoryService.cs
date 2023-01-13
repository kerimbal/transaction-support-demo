using OrderSvc.Data;
using OrderSvc.Entity;

namespace OrderSvc.Services;

public class CustomerOrderHistoryService
{
    private readonly AppDbContext _dbContext;
    public CustomerOrderHistoryService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(CustomerOrderHistory customerOrderHistory)
    {
        _dbContext.CustomerOrderHistories.Add(customerOrderHistory);
        _dbContext.SaveChanges();
    }
}