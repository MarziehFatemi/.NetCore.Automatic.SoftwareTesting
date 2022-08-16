using Onion.Application.Contracts;
using Onion.Domain.Product_Category_agg;
using Onion.Domain.Tests.ProductCategoryTestAggs;
using FluentAssertions;
using NSubstitute;

namespace Onion.Application.Tests.Unit
{
    public class ProductCategoryTests
    {
         private readonly ProductCategoryApplication _Service;
        private readonly IProductCategoryRepository _Repository;

        public ProductCategoryTests()
        {
            _Repository = Substitute.For<IProductCategoryRepository>();
            _Service = new ProductCategoryApplication(_Repository);
        }

        [Fact]
        public void Should_GetListOfProductCategories()
        {
            //arrange
            _Repository.GetAll().Returns(new List<ProductCategory>());

            //act
            var ProductCategories = _Service.GetAll();

            //assert
            _Repository.Received().GetAll();
            ProductCategories.Should().BeOfType<List<ProductCategoryViewModel>>();
            
        }


        [Fact]
        public void Should_CreateANewProductCategory()
        {
            //arrange
            var command = CreateSomeProductCategory();

            string Error = "";
            //act
            _Service.Create(command, out Error);

            //assert
           
            _Repository.ReceivedWithAnyArgs().Create(default);
           
        }

        private CreateProductCategoryCommand CreateSomeProductCategory()
        {
            //Arrange
            var Command = new CreateProductCategoryCommand
            {
                Name = "Electronic Device", // Guid.NewGuid().ToString(),
            };
            return Command;
        }


        [Fact]
        public void Should_Edit_Existing_ProductCategory()
        {
            //Arrange
            var Command = new EditProductCategoryCommand(1, "Carpets");
            string Error = "";

            //Act 
            _Service.Edit(Command, out Error);


            //Assert 
            _Repository.Received().Edit(Command.Id,Command.Name);

        }


        [Fact]
        public void Should_Get_AProductCategory_byId()
        {
            //arrange 
            int id = 2;
            string Error = "";


            // act 
            _Service.GetEntity(id, out Error);

            // Assert

            _Repository.Received().Get(id);
        }



    }
}