using Data;
using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

string ConnectionString = "Server=localhost;Database=PsscDb;Trusted_Connection=True;TrustServerCertificate=True";


var dbContextBuilder = new DbContextOptionsBuilder<EcommerceAppDbContext>()
                                                .UseSqlServer(ConnectionString).Options;

using (var dbContext = new EcommerceAppDbContext(dbContextBuilder))
{
    dbContext.Database.Migrate();
}