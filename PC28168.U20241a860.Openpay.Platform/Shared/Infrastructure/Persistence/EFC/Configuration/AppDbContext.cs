using Microsoft.EntityFrameworkCore;
using PC28168.U20241a860.Openpay.Platform.Disbursements.Domain.Model.Aggregates;
using PC28168.U20241a860.Openpay.Platform.Settlements.Domain.Model.ValueObjects;
using PC28168.U20241a860.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using PC28168.U20241a860.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Interceptors;

namespace PC28168.U20241a860.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<DisbursementOrder> DisbursementOrders => Set<DisbursementOrder>();

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<DisbursementOrder>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.ComplexProperty(e => e.OrderTrackingCode, b =>
            {
                b.Property(o => o.Value)
                    .IsRequired()
                    .HasColumnName("OrderTrackingCode");
            });

            entity.HasIndex(e => e.OrderTrackingCode.Value).IsUnique();

            entity.ComplexProperty(e => e.AffiliateStoreId, b =>
            {
                b.Property(s => s.Value)
                    .IsRequired()
                    .HasColumnName("AffiliateStoreId");
            });

            entity.ComplexProperty(e => e.ExecutionSchedule, b =>
            {
                b.Property(d => d.ScheduledFrom).IsRequired().HasColumnName("ScheduledFrom");
                b.Property(d => d.ScheduledTo).IsRequired().HasColumnName("ScheduledTo");
            });

            entity.ComplexProperty(e => e.TransferCap, b =>
            {
                b.Property(f => f.Amount).IsRequired().HasColumnType("decimal(18,2)").HasColumnName("TransferCapAmount");
                b.Property(f => f.CurrencyCode).IsRequired().HasMaxLength(3).HasColumnName("TransferCapCurrencyCode");
            });

            entity.Property(e => e.Status)
                .IsRequired();

            entity.Property(e => e.User)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.Remarks)
                .HasMaxLength(85)
                .IsRequired(false);
        });

        // Aplica la convención snake_case globalmente a las tablas y columnas
        builder.UseSnakeCaseNamingConvention();
    }
}