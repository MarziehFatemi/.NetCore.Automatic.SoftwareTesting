using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onion.Domain.Product_Category_agg;

namespace Onion.Infrastructure.EfCore.Mapping
{
    public class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory> // name of the model entity
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("Tb_ProductCategories"); // name of the table 
            builder.HasKey(x => x.Id); // set the primary key 
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired(); // 


            builder.HasMany(x => x.Products)
           .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);
        }
    }
}
