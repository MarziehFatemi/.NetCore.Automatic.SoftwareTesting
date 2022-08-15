using FluentAssertions;
using Onion_Domain.Product_agg.Exceptions;

namespace Onion.Domain.Tests.ProductTestAggs
{
    public class ProductTests : IDisposable
    {
        private readonly ProductTestBuilder productTestBuilder;

        public ProductTests()
        {
            productTestBuilder = new ProductTestBuilder();
        }

        [Fact]
        public void Constructor_Should_Construct_Properly()
        {
            // arrange 
           

            // act 

            var product = productTestBuilder.Build();

            // assert 

            product.Name.Should().Be(productTestBuilder.Name);
            product.UintPrice.Should().Be(productTestBuilder.UintPrice);
            product.CategoryId.Should().Be(productTestBuilder.CategoryId);

        }


        [Fact]
        public void Edit_Should_Change_NameUniPriceCategoryID_2NewOne()
        {
            // arrange 
            var product = productTestBuilder.Build();

            string UpdatedName = Guid.NewGuid().ToString();
            int UpdatedCategoryId = new Random().Next();
            int UpdatedUnitPrice = new Random().Next();


            // act 
            product.Edit(UpdatedUnitPrice,UpdatedName, UpdatedCategoryId);

            // assert 

            product.Name.Should().Be(UpdatedName);
            product.UintPrice.Should().Be(UpdatedUnitPrice);
            product.CategoryId.Should().Be(UpdatedCategoryId);

        }


        ////[Fact]
        ////public void Should_ThrowException_When_NameAndProductId_Is_Repeated()
        ////{


        ////}

        ////[Theory]
        ////[InlineData(null)]
        ////[InlineData("")]
        ////public void Should_ThrowException_When_Name_Is_NullOrWhiteSpace(string TestName)
        ////{
        ////    // arrange 

        ////    // act 

        ////    Action Actual = () => productTestBuilder.WithName(TestName).Build();

        ////    // assert 
        ////    Actual.Should().ThrowExactly<ProductNameIsInvalidException>();


        ////}


        ////[Theory]
        ////[InlineData(null)]
        ////[InlineData(0)]
        ////[InlineData(-10)]
        ////public void Should_ThrowException_When_UnitPrice_Is_NullOrMinusOrZero(int TestUnitPrice)
        ////{
        ////    // arrange 

        ////    // act 

        ////    Action Actual = () => productTestBuilder.WithUnitPrice(TestUnitPrice).Build();

        ////    // assert 
        ////    Actual.Should().ThrowExactly<ProductUnitPriceIsInvalidException>();


        ////}

        ////[Theory]
        ////[InlineData(null)]
        ////[InlineData(0)]
        ////[InlineData(-10)]
        ////public void Should_ThrowException_When_CategoryId_Is_NullOrOrMinusOrZero(int TestCategoryId)
        ////{
        ////    // arrange 
        ////    var productTestBuilder = new ProductTestBuilder();

        ////    // act 

        ////    Action Actual = () => productTestBuilder.WithCategoryId(TestCategoryId).Build();

        ////    // assert 
        ////    Actual.Should().ThrowExactly<ProductCategoryIdIsInvalidException>();


        ////}

        public void Dispose()
        {
            
        }
    }
}