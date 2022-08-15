using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onion.Domain.Product_agg;

namespace Onion.Infrastructure.EfCore.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product> // name of the model entity
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Tb_Products"); // name of the table 
            builder.HasKey(x => x.Id); // set the primary key 
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired(); // 

            builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId);


            
        }
    }

}
