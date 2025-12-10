using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Supply> Supply { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
}