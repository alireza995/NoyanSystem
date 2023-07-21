using Basic.Context;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext : GenericDataContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
           .Property(e => e.BirthDate)
           .HasConversion(
                v => v.HasValue ?
                    v.Value.ToDateTime(TimeOnly.MinValue) :
                    (DateTime?)null,
                v => v.HasValue ?
                    DateOnly.FromDateTime(v.Value) :
                    null
            );
    }
}