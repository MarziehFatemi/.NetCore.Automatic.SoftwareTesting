using Onion.Domain.Product_Category_agg;


namespace Onion.Domain.Tests.ProductCategoryTestAggs
{
    public  class ProductCategoryTestBuilder
    {
       public string Name { get; private set; } = "Bussiness Coaching Training";

        public  ProductCategoryTestBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public  ProductCategory Build()
        {
            return new ProductCategory(Name);
        }

    }
}
