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
        private string RemovePath = "/api/ProductCategory/Remove";

        private RESTFulApiFactoryClient _restClient;

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
            actual.Should().BeOfType<List<ProductCategoryViewModel>>();

        }

        [Fact]
        public async void Should_Return_ProductCategoryBy()
        {
            //arrange
            string Name = Guid.NewGuid().ToString();

            var CreationResult = CreateSampleProductCategory(Name).Result;


            //act 
            var actual = await _restClient.GetContentAsync<EditProductCategoryCommand>($"{ListPath}/{CreationResult.Id}");

            //assert
            actual.Id.Should().BeGreaterThan(0);
            actual.Id.Should().Be(CreationResult.Id);
            actual.Name.Should().Be(Name);

            // tear down 
            await _restClient.GetContentAsync<ResultStatus>($"{RemovePath}/{CreationResult.Id}");


        }

        [Fact]
        public async void Should_Edit_ProductCategory()
        {
            //arrange
            string Name = Guid.NewGuid().ToString();

            var CreationResult = CreateSampleProductCategory(Name).Result;

            var EditCommand = new EditProductCategoryCommand()
            {
                Id = CreationResult.Id,
                Name = Guid.NewGuid().ToString(),
            };
        

            //act 
            var EditResult = await _restClient.PostContentAsync<EditProductCategoryCommand,ResultStatus>(EditPath, EditCommand);
            
            var EditedEntity = await _restClient.GetContentAsync<EditProductCategoryCommand>($"{ListPath}/{CreationResult.Id}");

            //assert
            EditResult.IsOk.Should().BeTrue();
            EditedEntity.Id.Should().Equals(CreationResult.Id);
            EditedEntity.Name.Should().Be(EditCommand.Name);


            // tear down 
            await _restClient.GetContentAsync<ResultStatus>($"{RemovePath}/{CreationResult.Id}");



        }

        [Fact]
        public async void Should_CreateNewProductCategory()
        {
            //arrange
            string Name = Guid.NewGuid().ToString();

            var ResultStatus = CreateSampleProductCategory(Name).Result;

            //assert
            ResultStatus.IsOk.Should().BeTrue();
            var actual = await _restClient.GetContentAsync<EditProductCategoryCommand>($"{ListPath}/{ResultStatus.Id}");
            actual.Name.Equals(Name);

            // tear down 
            await _restClient.GetContentAsync<ResultStatus>($"{RemovePath}/{ResultStatus.Id}");



            // var ProductCategories = await _restClient.GetContentAsync<List<ProductCategoryViewModel>>(ListPath);

            //ProductCategories.Should().ContainSingle(x => x.Id == ResultStatus.Id);
            ////ProductCategories[ResultStatus.Id].Name.Should().Equals(command.Name); 
            // await _restClient.DeleteContentAsync($"{ListPath}/{id}");
        }


        [Fact]
        public async void Should_Not_CreateRepeatitiveProductCategory_AndReturnMessage()
        {
            //arrange
            string Name = Guid.NewGuid().ToString();

            //act
            var ResultStatus1 = CreateSampleProductCategory(Name).Result;
            var ResultStatus2 = CreateSampleProductCategory(Name).Result;


            //assert
            ResultStatus2.IsOk.Should().BeFalse();
            ResultStatus2.Id.Should().Be(0);
            ResultStatus2.Error.Should().Be(ProductCategoryMessages.RepeatitiveNameError);


            // tear down 
            await _restClient.GetContentAsync<ResultStatus>($"{RemovePath}/{ResultStatus1.Id}");
        }

        [Fact]
        public async void Should_Delete_Product()
        {
            //arrange
            string Name = Guid.NewGuid().ToString();    
            var ResultStatus = CreateSampleProductCategory(Name).Result;



            //act 

            var actual = await _restClient.GetContentAsync<ResultStatus>($"{RemovePath}/{ResultStatus.Id}");



            //assert
            actual.Error.Should().Be(ProductCategoryMessages.SuccessfullyDeleted);
            actual.IsOk.Should().BeTrue();


            //act and assert 2
            var DeletedFromTestActual = await _restClient.GetContentAsync<EditProductCategoryCommand>($"{ListPath}/{ResultStatus.Id}");
            DeletedFromTestActual.Should().BeNull();

        }


        public async Task<ResultStatus> CreateSampleProductCategory(string name)
        {
            var command = new CreateProductCategoryCommand()
            {
                Name = name,
            };



            //act
            var ResultStatus = await _restClient.PostContentAsync<CreateProductCategoryCommand, ResultStatus>(CreatePath, command);

            return ResultStatus;
        }



    }
}