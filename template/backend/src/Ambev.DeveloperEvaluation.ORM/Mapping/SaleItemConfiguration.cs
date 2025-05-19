using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItemEntity>
{
    public void Configure(EntityTypeBuilder<SaleItemEntity> builder)
    {
        builder.ToTable("SalesItem");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(c => c.Product)
            .HasColumnName("Product")
            .HasColumnType("varchar(50)")
            .HasDefaultValue("");

        builder.Property(u => u.Quantity).IsRequired();

        builder.Property(o => o.UnitPrice)
            .HasColumnName("UnitPrice")
            .HasColumnType("decimal(18,2)");

        builder.Property(o => o.Discount)
            .HasColumnName("Discount")
            .HasColumnType("decimal(18,2)");

        builder.Property(o => o.TotalAmount)
            .HasColumnName("TotalAmount")
            .HasColumnType("decimal(18,2)");
    }
}
