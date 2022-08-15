﻿using RESTFulSense.Clients;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Onion.Application.Contracts.ProductApplication_Agg;
using TestApi.Controllers;

namespace Onion.PresentationApi.Tests.Integration
{
    public class ProductControllerTests
    {
        private string ListPath = "/api/Product";
        private string CreatePath = "/api/Product/CreateProduct";
        private string EditPath = "/api/Product/EditProdcut";
        private string DeActivatePath = "/api/Product/DeActivate";
        private string ActivatePath = "/api/Product/Activate";

        private RESTFulApiFactoryClient _restClient;

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
            var actual = await _restClient.GetContentAsync<List<ProductViewModel>>(ListPath);

            //assert
            actual.Should().HaveCountGreaterOrEqualTo(0);

        }


        [Theory]
         ////[InlineData(0)]
         ////[InlineData(-1)]
         [InlineData(2)]
        //[InlineData(100)]
        public async void Should_Return_ProductBy(int Id)
        {

            //act 
            var actual = await _restClient.GetContentAsync<EditProductCommand>($"{ListPath}/{Id}");

            
            // Assert
            actual.Id.Should().BeGreaterThan(0);
            

        }


        [Fact]
        public  void Should_CreateNewProduct()
        {
            //arrange
            var resultStatus =  CreateSampleProductToTest();


            //assert
            resultStatus.Result.IsOk.Should().BeTrue();
        }


        [Fact]
        public async void Should_Edit_Product()
        {
           
            //arrange
            var resultStatus = CreateSampleProductToTest();

            var EditCommand = new EditProductCommand()
            {
                Id = resultStatus.Id,

                // Editting name and price
                CategoryId = 2,
                UnitPrice = new Random().Next(1, 2000),
                Name = Guid.NewGuid().ToString(),
            }; 


            //act 
            var ResultStatus = await _restClient.PostContentAsync<EditProductCommand, ResultStatus>(EditPath, EditCommand);
            //var actual = await _restClient.GetContentAsync<EditProductCommand>($"{ListPath}/{Id}");

            //assert
            ResultStatus.IsOk.Should().BeTrue();
            ////actual.Id.Should().Equals(Id);
            ////actual.Name.Should().Be(UpdatedName);


        }


        [Fact]
        public async void Should_DeActivate_Product()
        {
          
            //arrange
            var resultStatus = CreateSampleProductToTest();


            //act 
            var actual = await _restClient.GetContentAsync< ResultStatus >($"{DeActivatePath}/{resultStatus.Id}");

            //assert
            actual.IsOk.Should().BeTrue();

        }

        [Fact]
        public async void Should_Activate_Product()
        {
            //arrange
            var resultStatus = CreateSampleProductToTest();

            //act 
            var actual = await _restClient.GetContentAsync<ResultStatus>($"{ActivatePath}/{resultStatus.Id}");

            //assert
            actual.IsOk.Should().BeTrue();

        }



        public async Task<ResultStatus> CreateSampleProductToTest()
        {
            var command = new CreateProductCommand()
            {
                Name = Guid.NewGuid().ToString(),
                CategoryId = 2, // there are some unavailable category Ids!!! 
                UnitPrice = 110,

            };



            //act
            var resultStatus = await _restClient.PostContentAsync<CreateProductCommand, ResultStatus>(CreatePath, command);

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
