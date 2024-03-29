using DynatronCustomerCRUD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var configuration = builder.Configuration;

// Add User Secrets in development
#if DEBUG
builder.Configuration.AddUserSecrets<DynatronDbContext>();
#endif

//builder.Services.AddDbContext<DynatronDbContext>();
builder.Services.AddDbContext<DynatronDbContext>(options =>
    options.UseMySql(configuration.GetConnectionString("DynatronDatabase"),
    ServerVersion.AutoDetect(configuration.GetConnectionString("DynatronDatabase"))));


// temp mock data solution until EF DB
builder.Services.AddSingleton(new List<Customer>
{
    new Customer { Id = 1, FirstName = "Bob", LastName = "Dillon", Email = "bobdillon@example.com", LastUpdatedDate = DateTime.Now },
    new Customer { Id = 2, FirstName = "Elroy", LastName = "Smith", Email = "elroysmith@example.com", LastUpdatedDate = DateTime.Now },
    new Customer { Id = 3, FirstName = "Tai", LastName = "Xiaoma", Email = "taixiaoma@example.com", LastUpdatedDate = DateTime.Now },
    new Customer { Id = 4, FirstName = "Kevin", LastName = "Bacon", Email = "kevinbacon@example.com", LastUpdatedDate = DateTime.Now },
    new Customer { Id = 5, FirstName = "Alena", LastName = "Zarcovia", Email = "alenazarcovia@example.com", LastUpdatedDate = DateTime.Now }
});
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
