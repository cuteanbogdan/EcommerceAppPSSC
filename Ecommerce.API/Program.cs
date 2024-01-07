using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EcommerceAppDbContext>(opt =>
{
    opt.UseSqlServer("Server=localhost;Database=PsscDb1;Trusted_Connection=True;TrustServerCertificate=True");
});
using (var dbContext = new EcommerceAppDbContext())
{
    dbContext.Database.Migrate();
}
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
