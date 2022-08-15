using Microsoft.EntityFrameworkCore;
using Onion.Domain.Product_agg;
using Onion.Domain.Product_Category_agg;
using Onion.Infrastructure.EfCore.Mapping;

namespace Onion.Infrastructure.EfCore
{
    public class Context : DbContext
    {
        public DbSet<Product> products { get; set; }
        public DbSet<ProductCategory> productCategories { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            //modelBuilder.ApplyConfiguration(new ProductMapping());
            //modelBuilder.ApplyConfiguration(new ProductCategoryMapping());

            var assembly = typeof(ProductMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);


            base.OnModelCreating(modelBuilder);
        }


    }
}
