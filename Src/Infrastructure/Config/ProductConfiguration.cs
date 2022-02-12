using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //Validation Config
            builder.Property(x => x.UserId)
                    .IsRequired();

            builder.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(x => x.Description)
                    .IsRequired()
                    .HasMaxLength(500);

            builder.Property(x => x.Price)
                    .HasColumnType("decimal(18,2)");

            //Navigation Config
            builder.HasOne(x => x.Brand)
                    .WithMany()
                      .HasForeignKey(x => x.BrandId);

            builder.HasOne(x => x.Category)
                    .WithMany()
                      .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.Color)
                    .WithMany()
                      .HasForeignKey(x => x.ColorId);

            builder.HasOne(x => x.ConditionsOfProduct)
                    .WithMany()
                      .HasForeignKey(x => x.ConditionsOfProductId);

            builder.HasMany(x => x.Offers).WithOne(x => x.Product);

        }
    }
}
