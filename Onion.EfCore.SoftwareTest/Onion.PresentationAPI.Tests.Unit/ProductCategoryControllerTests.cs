using Onion.Application.Contracts;
using TestApi.Controllers;
using FluentAssertions;
using NSubstitute;

namespace Onion.PresentationAPI.Tests.Unit
{
    public class ProductCategoryControllerTests
    {

        private ProductCategoryController Controller;
        private readonly IProductCategoryApplication Service;

        public ProductCategoryControllerTests()
        {
            // Arrange 
             Service = NSubstitute.Substitute.For<IProductCategoryApplication>();
             
             Controller = new ProductCategoryController(Service);


        }
        [Fact]
        public void Should_GetProductCategoryList()
        {
            // Arrange 
           

            //Act 
            Controller.GetProductCategoryList();


            //Assert 
            Service.Received().GetAll();


        }

        [Fact]
        public void Should_ReturnListOfCategoryViewModels()
        {
            // Arrange 
            Service.GetAll().Returns(new List<ProductCategoryViewModel>());

            //Act 
            var Actual = Controller.GetProductCategoryList();

            //Assert 
            Actual.Should().BeOfType<List<ProductCategoryViewModel>>(); 


        }

        [Fact]
        public void Should_Create_ProductCategory()
        {
            //Arrange
            var Command = new CreateProductCategoryCommand
            {
                Name = "Electronic Device", // Guid.NewGuid().ToString(),
            };
            string Error = "";

            //Act 
            Controller.PostCreateProductCategory(Command);


            //Assert 
            Service.Received().Create(Command,out Error);


        }

        [Fact]
        public void Should_Edit_Existing_ProductCategory()
        {
            //Arrange
            var Command = new EditProductCategoryCommand (1, "Carpets");
            string Error = "";

            //Act 
            Controller.PostEditProdcutCategory(Command);


            //Assert 
            Service.Received().Edit(Command, out Error);

        }


        [Fact]
        public void Should_Get_AProductCategory_byId()
        {
            //arrange 
            int id = 2;


            // act 
            Controller.GetProductCategory(id);

            // Assert

            Service.Received().GetEntity(id);
        }
    }
}