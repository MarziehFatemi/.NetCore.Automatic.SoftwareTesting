using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Onion.Application.Contracts;
using Onion.Application.Contracts.ProductCategoryApplicationAgg;
using RESTFulSense.Clients;
using TestApi.Controllers;

namespace Onion.PresentationApi.Tests.Integration
{
    public class ProductCategoryControllerTests
    {

        private string ListPath = "/api/ProductCategory";
        private string CreatePath = "/api/ProductCategory/CreateProductCategory";
        private string EditPath = "/api/ProductCategory/EditProdcutCategory"; 
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
            
            //act 
            var actual = await _restClient.GetContentAsync<List<ProductCategoryViewModel>>(ListPath);

            //assert
            actual.Should().HaveCountGreaterOrEqualTo(0);

        }

        [Theory]
       // [InlineData(0)]
       // [InlineData(-1)]
        [InlineData(2)]
       // [InlineData(100)]
        public async void Should_Return_ProductCategoryBy(int Id)
        {

            //act 
            var actual = await _restClient.GetContentAsync<EditProductCategoryCommand>($"{ListPath}/{Id}");

            //assert
            actual.Id.Should().BeGreaterThan(0);

        }

        [Theory]
        [InlineData(2, "Home Appliances")]
        public async void Should_Edit_ProductCategory(int Id, string UpdatedName)
        {
            // arrange 
            EditProductCategoryCommand EditCommand = await _restClient.GetContentAsync<EditProductCategoryCommand>($"{ListPath}/{Id}");
            EditCommand.Name = UpdatedName;

            //act 
            var ResultStatus = await _restClient.PostContentAsync<EditProductCategoryCommand,ResultStatus>(EditPath, EditCommand);
            var actual = await _restClient.GetContentAsync<EditProductCategoryCommand>($"{ListPath}/{Id}");

            //assert
            ResultStatus.IsOk.Should().BeTrue();
            actual.Id.Should().Equals(Id);
            actual.Name.Should().Be(UpdatedName);


        }

        [Fact]
        public async void Should_CreateNewProductCategory()
        {
            //arrange

            var command = new CreateProductCategoryCommand()
            {
                Name = Guid.NewGuid().ToString(),
            };
               
        
           
            //act
            var ResultStatus = await _restClient.PostContentAsync<CreateProductCategoryCommand, ResultStatus>(CreatePath, command);
            

            //assert
            ResultStatus.IsOk.Should().BeTrue();
            var actual = await _restClient.GetContentAsync<EditProductCategoryCommand>($"{ListPath}/{ResultStatus.Id}");
            actual.Name.Equals(command.Name);


            // var ProductCategories = await _restClient.GetContentAsync<List<ProductCategoryViewModel>>(ListPath);

            //ProductCategories.Should().ContainSingle(x => x.Id == ResultStatus.Id);
            ////ProductCategories[ResultStatus.Id].Name.Should().Equals(command.Name); 
            // await _restClient.DeleteContentAsync($"{ListPath}/{id}");
        }


        [Fact]
        public async void Should_Not_CreateRepeatitiveProductCategory_AndReturnMessage()
        {
            //arrange

            var command1 = new CreateProductCategoryCommand()
            {
                Name = Guid.NewGuid().ToString(),
            };
            var command2 = new CreateProductCategoryCommand()
            {
                Name = command1.Name,
            };



            //act
            var ResultStatus1 = await _restClient.PostContentAsync<CreateProductCategoryCommand, ResultStatus>(CreatePath, command1);
            var ResultStatus2 = await _restClient.PostContentAsync<CreateProductCategoryCommand, ResultStatus>(CreatePath, command2);


            //assert
            ResultStatus2.IsOk.Should().BeFalse();
            ResultStatus2.Id.Should().Be(0);
            ResultStatus2.Error.Should().Be(ProductCategoryMessages.RepeatitiveNameError);
           
            // await _restClient.DeleteContentAsync($"{ListPath}/{id}");
        }


    }
}