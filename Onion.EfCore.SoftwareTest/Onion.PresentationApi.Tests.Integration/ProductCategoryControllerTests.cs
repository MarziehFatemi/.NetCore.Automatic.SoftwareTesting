using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Onion.Application.Contracts;
using RESTFulSense.Clients;

namespace Onion.PresentationApi.Tests.Integration
{
    public class ProductCategoryControllerTests
    {

        private string ListPath = "/api/ProductCategory";
        private string CreatePath = "/api/ProductCategory/CreateProductCategory";
        private  RESTFulApiFactoryClient _restClient;

        public ProductCategoryControllerTests()
        {
            var applicationFactory = new WebApplicationFactory<Program>();
            var httpClient = applicationFactory.CreateClient();
            _restClient = new RESTFulApiFactoryClient(httpClient);


        }

        [Fact]
        public async void Should_Return_All_ProductCategories()
        {
            // arrange 
           
           

            //act 
            var actual = await _restClient.GetContentAsync<List<ProductCategoryViewModel>>(ListPath);

            //assert
            actual.Should().HaveCountGreaterOrEqualTo(0);

        }

        [Fact]
        public async void Should_CreateNewProductCategory()
        {
            //arrange

            var command = new CreateProductCategoryCommand()
            {
                Name = "salam",
            };
               
        
           
            //act
            var id = await _restClient.PostContentAsync<CreateProductCategoryCommand, int>(CreatePath, command);
            var ProductCategories = await _restClient.GetContentAsync<List<ProductCategoryViewModel>>(ListPath);

            //assert
            ProductCategories.Should().ContainSingle(x => x.Id == id);
           // await _restClient.DeleteContentAsync($"{ListPath}/{id}");
        }

       
    }
}