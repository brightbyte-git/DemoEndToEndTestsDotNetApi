using E2EDemoUserRegistration.Model;
using Microsoft.EntityFrameworkCore;

namespace E2EDemoUserRegistration;

public class E2EDemoDbContext : DbContext
{
    public E2EDemoDbContext(DbContextOptions<E2EDemoDbContext> options) : base(options) { }
    
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure Tenant relationships
        modelBuilder.Entity<Tenant>()
            .HasMany(t => t.Users)
            .WithOne(u => u.Tenant)
            .HasForeignKey(u => u.TenantId);
    }
}