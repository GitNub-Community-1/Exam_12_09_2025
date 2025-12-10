using Infastructure.Data;
using Infastructure.Interface;
using Infastructure.MapperProfile;
using Infastructure.MiddleWares;
using Infastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:7000");

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(conf => conf.UseNpgsql(connection));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ISupplyService, SupplyService>();
builder.Services.AddScoped<IProductSupplierService, ProductSupplierService>();
//builder.Services.AddScoped<IBookService, BookService>();
//builder.Services.AddScoped<IAuthorService, AuthorService>();
//builder.Services.AddScoped<IOrderService, OrderService>();
var app = builder.Build();
// Configure the HTTP request pipeline.

app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ErrorCheck_AndLog_MiddleWare>();
app.MapControllers();

// app.UseHttpsRedirection();


using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await dbContext.Database.GetInfrastructure().GetService<IMigrator>()!.MigrateAsync();
app.Run();