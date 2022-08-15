using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Onion.Domain.Tests.ProductTestAggs;
using Onion.Infrastructure.EfCore;
using Onion.Infrastructure.EfCore.Repository;
using Onion_Domain.Product_agg.Exceptions;

namespace Onion.Infrastructure.Tests.Integratioins
{
    public class ProductRepositoryTest : IClassFixture<RealDatabaseFixture>
    {
        private readonly ProductRepository _productRepository;
        private readonly ProductTestBuilder productTestBuilder;
        public ProductRepositoryTest( RealDatabaseFixture databaseFixture)
        {
            _productRepository =new ProductRepository(databaseFixture.TestContext);
            productTestBuilder = new ProductTestBuilder(); 
        }
        [Fact]
        public void Should_Return_All_Product()
        {

            // arrange 
           
            // act 
            var Actual = _productRepository.GetAll();

            // assert 
            Actual.Should().HaveCountGreaterThanOrEqualTo(0);
        }



        [Fact]
        public void Should_Create_Product()
        {
            // arrange 
            var Product = productTestBuilder.Build(); 

            //act 

            _productRepository.Create(Product);
            _productRepository.SaveChanges();
            var Actual = _productRepository.GetAll();

            // assert 
            Actual.Should().Contain(Product);

        }


        [Theory]
        [InlineData(2)]
        public void Should_GetCourseByIdWhenIdIsInRange(int id)
        {

            var Actual = _productRepository.Get(id);


            Actual.Id.Should().Be(id);
        }

        [Theory]
        [InlineData(1000)]
        public void Should_ReturnNull_WhenCanNotFindOr_IdIsOutOfRange(int id)
        {

            Action Actual = () => _productRepository.Get(id);


            Actual.Should().ThrowExactly<ProductIdIsInvalidException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_trowException_WhenIdIsNegetiveOrZero(int id)
        {

            Action Actual = () => _productRepository.Get(id);

            Actual.Should().ThrowExactly<ProductIdIsInvalidException>();

        }


        [Fact]
        public void Should_ReturnTrue_WhenTheProductWithSameNameAndCategoryIdExist()
        {
            // arrange 
            var Product = productTestBuilder.Build();
            // Act 
            _productRepository.Create(Product);
            _productRepository.SaveChanges();

            var Actual = _productRepository.Exist(Product.Name,Product.CategoryId);

            Actual.Should().BeTrue();
        }


        [Theory]
        [InlineData("SomeUpdatedName")]
        public void Should_Edit_Product(string ExpectedName)
        {
            // arrange 
            var Product = productTestBuilder.Build();
            string Error = "";
            int UpdatedUnitPrice = new Random().Next(0, 100);


            _productRepository.Create(Product);
            _productRepository.SaveChanges();

            var Actual = _productRepository.Edit
                (Product.Id, UpdatedUnitPrice,ExpectedName, Product.CategoryId);
            
            _productRepository.SaveChanges();


            Actual.Should().BeTrue();
            Product.Name.Should().Be(ExpectedName);


        }



        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Edit_Should_ThrowException_WhenIdIsInvalid(int Id)
        {
            //Arrange
            int UnitPrice = 150;
            int CategoryId = 2;
            string SomeName = "SomeUpdatedName";
           
            //Act
            Action Actual = () => _productRepository.Edit
            (Id, UnitPrice, SomeName, CategoryId);


            Actual.Should().ThrowExactly<ProductIdIsInvalidException>();


        }

    }
}