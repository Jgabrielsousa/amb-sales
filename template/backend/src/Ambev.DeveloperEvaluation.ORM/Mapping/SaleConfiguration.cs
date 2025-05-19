using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<SaleEntity>
{
    public void Configure(EntityTypeBuilder<SaleEntity> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(c => c.SaleNumber)
           .HasColumnName("SaleNumber")
           .HasColumnType("varchar(50)")
           .HasDefaultValue("");

        builder.Property(o => o.SaleDate).IsRequired();

        builder.Property(u => u.Customer).IsRequired().HasMaxLength(50);

        builder.Property(u => u.Branch).HasMaxLength(20);

        builder.Property(o => o.IsCancelled).IsRequired();

        builder.HasMany(o => o.SalesItem)
                     .WithOne()
                     .HasForeignKey(oi => oi.SaleId)
                     .OnDelete(DeleteBehavior.Cascade);

    }
}
