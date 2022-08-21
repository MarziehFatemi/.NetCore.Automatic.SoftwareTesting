using Onion.Domain.Product_agg;
using Onion.Domain.Product_Category_agg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Domain.Tests.ProductTestAggs
{
    public class ProductTestBuilder 
    {
       public int UintPrice { get; private set; } = 1100;
       public string Name { get; private set; } = "Think and  grow rich";
       public int CategoryId { get; private set; } = ProductCategorySeed.ProductCategorySeedId;

        public ProductTestBuilder WithUnitPrice (int unitPrice)
        {
            UintPrice = unitPrice; 
            return this;
        }
        public ProductTestBuilder WithName(string name)
        {
            Name = name;
            return this;
        }
        public ProductTestBuilder WithCategoryId(int categoryId)
        {
            CategoryId = categoryId;
            return this;
        }

        public Product Build()
        {
            return new Product(UintPrice, Name, CategoryId); 
        }

    }
}
