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
    public class ConditionOfProductConfiguration : IEntityTypeConfiguration<ConditionsOfProduct>
    {
        public void Configure(EntityTypeBuilder<ConditionsOfProduct> builder)
        {
            builder.Property(x => x.Condition)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(x => x.Products).WithOne(x => x.ConditionsOfProduct);
        }
    }
}
