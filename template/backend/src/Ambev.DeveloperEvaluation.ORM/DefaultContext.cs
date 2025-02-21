using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }
    public DbSet<Sale> Sales { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sale>()
           .HasMany(s => s.Items)
           .WithOne()
           .OnDelete(DeleteBehavior.Cascade); // Exclui os itens quando a venda for excluída

        modelBuilder.Entity<SaleItem>()
            .Property(si => si.UnitPrice)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<SaleItem>()
            .Property(si => si.TotalAmount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<SaleItem>()
            .Property(si => si.Discount)
            .HasColumnType("decimal(18,2)");


        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(
               connectionString,
               b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.WebApi")
        );

        return new DefaultContext(builder.Options);
    }
}