using FluentAssertions;
using NSubstitute;
using Onion.Application.Contracts.ProductApplication_Agg;
using Onion.Domain.Product_agg;

namespace Onion.Application.Tests.Unit
{
    public class ProductTests
    {
        private readonly ProductApplication _Service;
        private readonly IProductRepository _Repository;

        public ProductTests()
        {
            _Repository = Substitute.For<IProductRepository>();
            _Service = new ProductApplication(_Repository);
        }

        [Fact]
        public void Should_GetListOfProducts()
        {
            //arrange
            _Repository.GetAll().Returns(new List<Product>());

            //act
            var Products = _Service.GetAll();

            //assert
            _Repository.Received().GetAll();
            Products.Should().BeOfType<List<ProductViewModel>>();

        }


        [Fact]
        public void Should_CreateANewProduct()
        {
            //arrange
            var command = CreateSomeProduct();

            string Error = "";
            //act
            _Service.Create(command, out Error);

            //assert

            _Repository.ReceivedWithAnyArgs().Create(default);

        }

        [Fact]
        public void Should_DeleteProduct()
        {
            string Error = "";

            //act
            _Service.Delete(2, out Error);

            //assert

            _Repository.ReceivedWithAnyArgs().Delete(default);

        }

        private CreateProductCommand CreateSomeProduct()
        {
            //Arrange
            
            var Command = new CreateProductCommand
            {
                Name = "Mac", // Guid.NewGuid().ToString(),
                CategoryId = 2,
                UnitPrice = 110,

            };
            
            return Command;
        }


        [Fact]
        public void Should_Edit_Existing_Product()
        {
            //Arrange
            var Command = new EditProductCommand(2, "Del", 1100, 2);

            string Error = "";

            //Act 
            _Service.Edit(Command, out Error);


            //Assert 
            _Repository.Received().Edit(Command.Id, Command.UnitPrice, Command.Name,Command.CategoryId);

        }


        [Fact]
        public void Should_Get_AProduct_byId()
        {
            //arrange 
            int id = 2;
            string Error = "";


            // act 
            _Service.GetBy(id,out Error);

            // Assert

            _Repository.Received().Get(id);
        }



        [Theory]
        [InlineData(2)]
        public void Should_DeActivate_Product(int Id)
        {
            string Error = "";
            //act 
            _Service.DeActivate(Id, out Error);


            //assert
            _Repository.ReceivedWithAnyArgs().DeActivate(default); 


        }

        [Theory]
        [InlineData(2)]
        public void Should_Activate_Product(int Id)
        {
            string Error = "";
            //act 
            _Service.Activate(Id, out Error);


            //assert
            _Repository.ReceivedWithAnyArgs().Activate(default);



        }







    }
}
