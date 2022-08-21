using RESTFulSense.Clients;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Onion.Application.Contracts.ProductApplication_Agg;
using TestApi.Controllers;
using Onion.Presentation.WebApi.Controllers;
using Onion.Domain.Product_Category_agg;

namespace Onion.PresentationApi.Tests.Integration
{
    public class ProductControllerTests
    {
     
        


        private RESTFulApiFactoryClient _restClient;
        CreateProductCommand SomeCreationCommand = new CreateProductCommand(); 

        public ProductControllerTests()
        {
            var applicationFactory = new WebApplicationFactory<Program>();
            var httpClient = applicationFactory.CreateClient();
            _restClient = new RESTFulApiFactoryClient(httpClient);
            


        }

        [Fact]
        public async void Should_Return_All_Products()
        {

            //act 
            var actual = await _restClient.GetContentAsync<List<ProductViewModel>>(ProductConstants.ListPath);

            //assert
            actual.Should().HaveCountGreaterOrEqualTo(0);

        }


        [Fact]
        public async void Should_Return_ProductBy()
        {
            //arrange
            var resultStatus = CreateSampleProductToTest().Result;


            //act 
            var actual = await _restClient.GetContentAsync<EditProductCommand>($"{ProductConstants.ListPath}/{resultStatus.Id}");


            // Assert
            
            actual.Id.Should().BeGreaterThan(0);
            actual.Id.Should().Be(resultStatus.Id);
            actual.Name.Should().Be(SomeCreationCommand.Name);
            actual.CategoryId.Should().Be(SomeCreationCommand.CategoryId);
            actual.UnitPrice.Should().Be(SomeCreationCommand.UnitPrice);

            // tear down 
            _restClient.GetContentAsync<ResultStatus>($"{ProductConstants.RemovePath}/{resultStatus.Id}");



        }


        [Fact]
        public  void Should_CreateNewProduct()
        {
            //arrange
            var resultStatus =  CreateSampleProductToTest().Result;


            //assert
            resultStatus.IsOk.Should().BeTrue();

            // tear down 
             _restClient.GetContentAsync<ResultStatus>($"{ProductConstants.RemovePath}/{resultStatus.Id}");

        }


        [Fact]
        public async void Should_Edit_Product()
        {
           
            //arrange
            var CreationResult = CreateSampleProductToTest().Result;

            var EditCommand = new EditProductCommand()
            {
                Id = CreationResult.Id,

                // Editting name and price
                CategoryId = ProductCategorySeed.ProductCategorySeedId,
                UnitPrice = new Random().Next(1, 2000),
                Name = Guid.NewGuid().ToString(),
            }; 


            //act 
            var EditResult = await _restClient.PostContentAsync<EditProductCommand, ResultStatus>(ProductConstants.EditPath, EditCommand);
           
            //assert
            EditResult.IsOk.Should().BeTrue();
            EditResult.Id.Should().Equals(CreationResult.Id);
            

            // tear down 
            await _restClient.GetContentAsync<ResultStatus>($"{ProductConstants.RemovePath}/{CreationResult.Id}");


        }


        [Fact]
        public async void Should_DeActivate_Product()
        {
          
            //arrange
            var resultStatus = CreateSampleProductToTest().Result;


            //act 
            var actual = await _restClient.GetContentAsync< ResultStatus >($"{ProductConstants.DeActivatePath}/{resultStatus.Id}");

            //assert
            actual.IsOk.Should().BeTrue();

            // tear down 
            await _restClient.GetContentAsync<ResultStatus>($"{ProductConstants.RemovePath}/{resultStatus.Id}");


        }

        [Fact]
        public async void Should_Activate_Product()
        {
            //arrange
            var resultStatus = CreateSampleProductToTest().Result;

            //act 
            var actual = await _restClient.GetContentAsync<ResultStatus>($"{ProductConstants.ActivatePath}/{resultStatus.Id}");

            //assert
            actual.IsOk.Should().BeTrue();

            // tear down 
            await _restClient.GetContentAsync<ResultStatus>($"{ProductConstants.RemovePath}/{resultStatus.Id}");

        }

        [Fact]
        public async void Should_Delete_Product()
        {

            //arrange
            var resultStatus = CreateSampleProductToTest().Result;


            //act 
          
            var actual = await _restClient.GetContentAsync<ResultStatus>($"{ProductConstants.RemovePath}/{resultStatus.Id}");

            

            //assert
            actual.Error.Should().Be(ProductMessages.SuccessfullyDeleted); 
            actual.IsOk.Should().BeTrue();


            //act and assert 2
            var DeletedFromTestActual = await _restClient.GetContentAsync<EditProductCommand>($"{ProductConstants.ListPath}/{resultStatus.Id}");
            DeletedFromTestActual.Should().BeNull();

        }


        public async Task<ResultStatus> CreateSampleProductToTest()
        {
             SomeCreationCommand = new CreateProductCommand()
            {
                Name = Guid.NewGuid().ToString(),
                CategoryId = 8, // there are some unavailable category Ids!!! 
                UnitPrice = 110,

            };



            //act
            var resultStatus = await _restClient.PostContentAsync<CreateProductCommand, ResultStatus>(ProductConstants.CreatePath, SomeCreationCommand);

           
            return resultStatus; 
        }

        

        //[Fact]
        //public async void Should_Not_CreateRepeatitiveProductCategory_AndReturnMessage()
        //{
        //    //arrange

        //    var command1 = new CreateProductCommand()
        //    {
        //        Name = Guid.NewGuid().ToString(),
        //    };
        //    var command2 = new CreateProductCommand()
        //    {
        //        Name = command1.Name,
        //    };



        //    //act
        //    var ResultStatus1 = await _restClient.PostContentAsync<CreateProductCommand, ResultStatus>(CreatePath, command1);
        //    var ResultStatus2 = await _restClient.PostContentAsync<CreateProductCommand, ResultStatus>(CreatePath, command2);


        //    //assert
        //    ResultStatus2.IsOk.Should().BeFalse();
        //    ResultStatus2.Id.Should().Be(0);
        //    ResultStatus2.Error.Should().Be(ProductCategoryMessages.RepeatitiveNameError);

        //    // await _restClient.DeleteContentAsync($"{ListPath}/{id}");
        //}

        ////[Theory]
        ////// [InlineData(0)]
        ////// [InlineData(-1)]
        ////[InlineData(2)]
        ////// [InlineData(100)]
        ////public async void Should_Return_ProductBy(int Id)
        ////{

        ////    //act 
        ////    var actual = await _restClient.GetContentAsync<EditProductCommand>($"{ListPath}/{Id}");

        ////    //assert
        ////    actual.Id.Should().BeGreaterThan(0);

        ////}

        //[Theory]
        //[InlineData(2, "Home Appliances")]
        //public async void Should_Edit_Product(int Id, string UpdatedName)
        //{
        //    // arrange 
        //    EditProductCommand EditCommand = await _restClient.GetContentAsync<EditProductCommand>($"{ListPath}/{Id}");
        //    EditCommand.Name = UpdatedName;

        //    //act 
        //    var ResultStatus = await _restClient.PostContentAsync<EditProductCommand, ResultStatus>(EditPath, EditCommand);
        //    var actual = await _restClient.GetContentAsync<EditProductCommand>($"{ListPath}/{Id}");

        //    //assert
        //    ResultStatus.IsOk.Should().BeTrue();
        //    actual.Id.Should().Equals(Id);
        //    actual.Name.Should().Be(UpdatedName);


        //}


    }
}
