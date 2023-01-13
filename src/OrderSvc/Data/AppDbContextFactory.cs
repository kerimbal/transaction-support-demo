using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OrderSvc.Data;

// ReSharper disable once UnusedMember.Global
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
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
}