using EmailSender;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrderSvc.Data;
using OrderSvc.Services;
using Xunit;

namespace OrderSvc.IntegrationTests
{
    public class OrderAppServiceTests
    {
        [Fact]
        public void PlaceOrder_ShouldSaveTheOrderAndLogTheHistory()
        {
            var dbContext = CreateDbContext();

            var sut = new OrderAppService(
                new OrderService(dbContext, new EmailService(GetSmtpConfig())),
                new CustomerOrderHistoryService(dbContext));

            var orderNumber = Guid.NewGuid().ToString();
            sut.PlaceOrder(orderNumber, 123, "Jack Sparrow", "jack_sparrow@blackpearl.com", "T-Shirt", 1100);

            var retrievedOrder = dbContext.Orders.SingleOrDefault(x => x.OrderNumber == orderNumber);
            Assert.NotNull(retrievedOrder);

            var retrievedHistory = dbContext.CustomerOrderHistories.SingleOrDefault(x => x.OrderId == retrievedOrder.Id);
            Assert.NotNull(retrievedHistory);
            
        }


        private static AppDbContext CreateDbContext()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = @".\MSSQLSERVER01",
                InitialCatalog = "TransactionScopeDemoDb",
                IntegratedSecurity = true,
                MultipleActiveResultSets = true
            };

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(builder.ConnectionString);

            return new AppDbContext(optionsBuilder.Options);
        }

        private static SmtpConfig GetSmtpConfig() =>
            new()
            {
                Host = "smtp.ethereal.email",
                Port = 587,
                From = "blaise36@ethereal.email",
                Password = "fFyfjdTemTgsTF2J8e"
            };
    }


}