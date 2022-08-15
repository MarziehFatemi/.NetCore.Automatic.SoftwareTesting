using Microsoft.EntityFrameworkCore;
using Onion.Application;
using Onion.Application.Contracts;
using Onion.Application.Contracts.ProductApplication_Agg;
using Onion.Domain.Product_agg;
using Onion.Domain.Product_Category_agg;
using Onion.Infrastructure.EfCore;
using Onion.Infrastructure.EfCore.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
builder.Services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddTransient<IProductAppication, ProductApplication>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<Context>(x => x.UseSqlServer
(builder.Configuration.GetConnectionString("DefaultConnection")));




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
