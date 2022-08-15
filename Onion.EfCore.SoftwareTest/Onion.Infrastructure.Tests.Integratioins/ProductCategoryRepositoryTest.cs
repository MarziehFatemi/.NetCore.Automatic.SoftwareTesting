using Microsoft.EntityFrameworkCore;
using Onion.Infrastructure.EfCore;
using Onion.Infrastructure.EfCore.Repository;
using FluentAssertions;
using Onion.Domain.Product_Category_agg;

namespace Onion.Infrastructure.Tests.Integratioins
{
    public class ProductCategoryRepositoryTest : IClassFixture<RealDatabaseFixture>
    {
        private readonly ProductCategoryRepository productCategoryRepository;

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

            var productCategory = new ProductCategory("Industrial Machine");

            //act 

            productCategoryRepository.Create(productCategory);
            productCategoryRepository.SaveChanges(out string Error);
            var Actual = productCategoryRepository.GetAll();

            // assert 
            Actual.Should().Contain(productCategory);

        }



    }
}
