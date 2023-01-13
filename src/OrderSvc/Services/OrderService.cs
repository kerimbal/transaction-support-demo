using System.Transactions;
using EmailSender;
using OrderSvc.Data;
using OrderSvc.Entity;

namespace OrderSvc.Services
{
    public class OrderService
    {
        private readonly AppDbContext _dbContext;
        private readonly EmailService _emailService;

        public OrderService(AppDbContext dbContext, EmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public void Add(Order order)
        {
            using (var trans = new TransactionScope(TransactionScopeOption.Suppress))
            {
                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();

                _emailService.Send(
                    subject: "Your Order",
                    body: $"Your order is created with number {order.OrderNumber}",
                    isBodyHtml: true,
                    to: new[] { order.Email });

                trans.Complete();
            }
        }
    }
}
