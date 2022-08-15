using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Onion.Infrastructure.EfCore;
using Onion.Infrastructure.EfCore.Repository;

namespace Onion.Infrastructure.Tests.Integratioins
{
    public class ProductRepositoryTest : IClassFixture<RealDatabaseFixture>
    {
        private readonly ProductRepository _productRepository;

        public ProductRepositoryTest( RealDatabaseFixture databaseFixture)
        {
            _productRepository =new ProductRepository(databaseFixture.TestContext);
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

        }

    }
}