using System.Transactions;
using OrderSvc.Entity;

namespace OrderSvc.Services
{
    public class OrderAppService
    {
        private readonly OrderService _orderService;
        private readonly CustomerOrderHistoryService _customerOrderHistoryService;

        public OrderAppService(OrderService orderService, CustomerOrderHistoryService customerOrderHistoryService)
        {
            _orderService = orderService;
            _customerOrderHistoryService = customerOrderHistoryService;
        }

        public void PlaceOrder(string orderNumber, int customerId, string customerName, string email, string productName, decimal price)
        {
            using (var trans = new TransactionScope())
            {
                // Save the order
                var order = new Order
                {
                    OrderNumber = orderNumber,
                    CustomerId = customerId,
                    ProductName = productName,
                    Email = email,
                    CustomerName = customerName,
                    Price = price
                };
                
                // async - await
                
                _orderService.Add(order);

                // Log the history
                var history = new CustomerOrderHistory
                {
                    OrderId = order.Id,
                    CustomerId = customerId,
                    CustomerName = customerName,
                    Price = price
                };

                _customerOrderHistoryService.Add(history);

                trans.Complete(); // Commit
            }
        }
    }
}
