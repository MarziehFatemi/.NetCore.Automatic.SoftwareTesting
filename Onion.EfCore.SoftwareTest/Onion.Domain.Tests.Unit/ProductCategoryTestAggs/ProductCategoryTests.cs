using FluentAssertions;
using Onion.Domain.Product_Category_agg;

namespace Onion.Domain.Tests.ProductCategoryTestAggs
{

    public class ProductCategoryTests : IDisposable
    {
        private readonly  ProductCategoryTestBuilder  productCategoryTestBuilder;

        public ProductCategoryTests()
        {
            productCategoryTestBuilder = new ProductCategoryTestBuilder();
        }

        [Fact]
        public void Constructor_Should_Construct_Properly()
        {
            // arrange 
             

            // act 

            var productCategory = productCategoryTestBuilder.Build();

            // assert 

            productCategory.Name.Should().Be(productCategoryTestBuilder.Name); 

        }

        [Fact]   
        public void Edit_Should_Change_Name_to_New_Name()
        {
            // arrange 
            var productCategory = productCategoryTestBuilder.Build();

            string NewName = Guid.NewGuid().ToString();

            // act 
            productCategory.Edit(NewName); 

            // assert 

            productCategory.Name.Should().Be(NewName);

        }


        //[Theory]
        //[InlineData(null)]
        //[InlineData("")]
        //public void Should_ThrowException_When_Name_Is_NullOrWhiteSpace(string TestName)
        //{
        //    // arrange 

        //    // act 

        //    Action productCategory = () => productCategoryTestBuilder.WithName(TestName).Build();

        //    // assert 
        //    productCategory.Should().ThrowExactly<ProductCategoryNameIsInvalidException>();

        //}



        //[Fact]
        //public void Should_ThrowException_When_Name_Is_Repeated()
        //{


        //}


        public void Dispose()
        {
            
        }




    }
}
