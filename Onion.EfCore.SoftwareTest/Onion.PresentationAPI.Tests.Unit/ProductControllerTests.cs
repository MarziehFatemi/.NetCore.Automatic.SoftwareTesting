using TestApi.Controllers;
using FluentAssertions;
using NSubstitute;
using Onion.Application.Contracts.ProductApplication_Agg;
using Onion.Application.Contracts;

namespace Onion.PresentationAPI.Tests.Unit
{
    public class ProductControllerTests
    {

        private ProductController Controller;
        private readonly IProductAppication Service;

        public ProductControllerTests()
        {
            // Arrange 
            Service = NSubstitute.Substitute.For<IProductAppication>();

            Controller = new ProductController(Service);


        }
        [Fact]
        public void Should_GetProductList()
        {
            // Arrange 


            //Act 
            Controller.GetProducts();


            //Assert 
            Service.Received().GetAll();


        }

        [Fact]
        public void Should_ReturnListOfProductViewModels()
        {
            // Arrange 
            Service.GetAll().Returns(new List<ProductViewModel>());

            //Act 
            var Actual = Controller.GetProducts();

            //Assert 
            Actual.Should().BeOfType<List<ProductViewModel>>();


        }

        [Fact]
        public void Should_Create_Category()
        {
            //Arrange
            var Command = new CreateProductCommand
            {
                Name = "Iphone", // Guid.NewGuid().ToString(),
                CategoryId = 2,
                UnitPrice = 110,

            };
            string Error = "";

            //Act 
            Controller.PostCreateProduct(Command);


            //Assert 
            Service.Received().Create(Command, out Error);


        }

        [Fact]
        public void Should_Edit_Existing_Product()
        {
            //Arrange
            var Command = new EditProductCommand(2,"Mac",1100,2);
            string Error = "";

            //Act 
            Controller.PostEditProdcut(Command);


            //Assert 
            Service.Received().Edit(Command, out Error);

        }


        [Theory]
        [InlineData(2)]
        public void Should_DeActivate_Product(int Id)
        {
            //act 
            Controller.GetDeActivate(Id);

            string Error = "";
            //assert
            Service.Received().DeActivate(Id, out Error); 


        }

        [Theory]
        [InlineData(2)]
        public void Should_Activate_Product(int Id)
        {
            //act 
            Controller.GetActivate(Id);

            string Error = "";
            //assert
            Service.Received().Activate(Id, out Error);


        }



    }
}
