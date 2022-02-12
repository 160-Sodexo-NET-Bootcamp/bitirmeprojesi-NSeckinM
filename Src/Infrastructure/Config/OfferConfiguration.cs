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
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            //Validation Config
            builder.Property(x => x.UserId)
                    .IsRequired();

            builder.Property(x => x.PercentageOfOffer)
                    .IsRequired();
            //Navigation Config
            builder.HasOne(x => x.Product)
                    .WithMany()
                      .HasForeignKey(x => x.ProductId);

        }
    }
}
