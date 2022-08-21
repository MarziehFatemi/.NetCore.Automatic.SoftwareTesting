using Microsoft.EntityFrameworkCore;
using Onion.Infrastructure.EfCore;
using Onion.Infrastructure.EfCore.Repository;
using FluentAssertions;
using Onion.Domain.Product_Category_agg;
using Onion.Domain.Tests.ProductCategoryTestAggs;
using Onion_Domain.Product_agg.Exceptions;

namespace Onion.Infrastructure.Tests.Integratioins
{
    public class ProductCategoryRepositoryTest : IClassFixture<RealDatabaseFixture>
    {
        
        private readonly ProductCategoryRepository productCategoryRepository;

        private ProductCategory productCategorySeed = new ProductCategory("Industrial Machine");

        public ProductCategoryRepositoryTest(RealDatabaseFixture databaseFixture)
        {
            productCategoryRepository = new ProductCategoryRepository(databaseFixture.TestContext);
        }



        [Fact]
        public void Should_Return_All_ProductCategories()
        {

            // arrange 
            

            // act 
            var Actual = productCategoryRepository.GetAll();

            // assert 
            Actual.Should().HaveCountGreaterThanOrEqualTo(1);
        }


        [Fact]
        public void Should_Create_ProductCategory()
        {
            // arrange 

           
            //act 

            productCategoryRepository.Create(productCategorySeed);
            productCategoryRepository.SaveChanges();
            var Actual = productCategoryRepository.GetAll();

            // assert 
            Actual.Should().Contain(productCategorySeed);

        }

        [Fact]
        public void Should_Delete_ProductCategory()
        {
            productCategoryRepository.Create(productCategorySeed);
            productCategoryRepository.SaveChanges();

            var Expected = productCategoryRepository.Get(productCategorySeed.Id);

            productCategoryRepository.Delete(Expected);
            productCategoryRepository.SaveChanges();

            var Actual = productCategoryRepository.GetAll();
            Actual.Should().NotContain(Expected);


        }


        [Fact]
        public void Should_GetProductCategoryByIdWhenIdIsInRange()
        {
            productCategoryRepository.Create(productCategorySeed);
            productCategoryRepository.SaveChanges();

            var Actual =  productCategoryRepository.Get(productCategorySeed.Id);


            Actual.Id.Should().Be(productCategorySeed.Id);
        }

        [Theory]
        [InlineData(1000)]
        public void Should_ReturnNull_WhenCanNotFindOr_IdIsOutOfRange(int id)
        {

            Action Actual = () => productCategoryRepository.Get(id);


            Actual.Should().ThrowExactly<ProductIdIsInvalidException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_trowException_WhenIdIsNegetiveOrZero(int id)
        {

            Action Actual = () => productCategoryRepository.Get(id);

            Actual.Should().ThrowExactly<ProductIdIsInvalidException>(); 
          
        }


        [Fact]
        public void Should_ReturnTrue_WhenTheProductCategoryWithSameNameExist()
        {

            // Act 
            productCategoryRepository.Create(productCategorySeed);
            productCategoryRepository.SaveChanges();

            var Actual = productCategoryRepository.Exist(productCategorySeed.Name);

            Actual.Should().BeTrue(); 
        }


        [Theory]
        [InlineData("SomeUpdatedName")]
        public void Should_Edit_ProductCategory(string ExpectedName)
        {
            string Error = "";
            productCategoryRepository.Create(productCategorySeed);
            productCategoryRepository.SaveChanges();

           var Actual= productCategoryRepository.Edit(productCategorySeed.Id, ExpectedName);
            productCategoryRepository.SaveChanges();


            Actual.Should().BeTrue();
            productCategorySeed.Name.Should().Be(ExpectedName);


        }

        

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Edit_Should_ThrowException_WhenIdIsInvalid(int Id)
        {
           
            Action Actual = () => productCategoryRepository.Edit(Id, "SomeUpdatedName");

           
            Actual.Should().ThrowExactly<ProductIdIsInvalidException>();


        }

    }
}
