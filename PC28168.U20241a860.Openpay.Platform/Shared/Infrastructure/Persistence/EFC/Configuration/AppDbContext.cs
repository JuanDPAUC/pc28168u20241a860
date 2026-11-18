using Microsoft.EntityFrameworkCore;
using PC28168.U20241a860.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using PC28168.U20241a860.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Interceptors;
using User = ;

namespace PC28168.U20241a860.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>()
            .Property(u => u.UserId)
            .HasConversion(valueObject => valueObject.Identifier, value => new UserId(value))
            .IsRequired();
        builder.Entity<User>()
            .HasIndex(u => new { u.UserId })
            .IsUnique();
        
        builder.UseSnakeCaseNamingConvention();
    }
}