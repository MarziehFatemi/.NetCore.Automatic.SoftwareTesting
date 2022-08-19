

using FluentAssertions;
using Onion.Application.Contracts.ProductApplication_Agg;
using Onion.Infrastructure.EfCore.Repository;
using Onion.Infrastructure.Tests.Integratioins;

namespace Onion.Application.Tests.Integrations
{
    public class ProductServiceTests : IClassFixture<RealDatabaseFixture>
    {
        private readonly ProductRepository _Repository;
        private readonly ProductApplication _Service;
        private CreateProductCommand CreateCommand; 
        //private Product _Seed = new Product("Industrial Machine");

        public ProductServiceTests(RealDatabaseFixture databaseFixture)
        {
            _Repository = new ProductRepository(databaseFixture.TestContext);

            _Service = new ProductApplication(_Repository);
        }



        [Fact]
        public void Should_Return_All_ProductCategories()
        {

            // act 
            var Actual = _Service.GetAll();

            // assert 
            Actual.Should().HaveCountGreaterThanOrEqualTo(1);
        }


        [Fact]
        public void Should_Create_Product()
        {
            // arrange 
              string Error = "";
            string Name = Guid.NewGuid().ToString();

            //act 

            int ActualId = CreateSomeProduct(Name,out Error);

           
            Error.Should().Be(ProductMessages.SuccessfullCreation);
            ActualId.Should().BeGreaterThan(0);

            var ActualEntity = _Service.GetBy(ActualId, out string Error2);

            ActualEntity.Name = CreateCommand.Name;
            ////var Actual = _Service.GetAll();

            ////// assert 
            ////Actual.Should().Contain(Expected);

        }

        [Fact]
        public void Should_Delete_ProductCategory()
        {
            string Name = Guid.NewGuid().ToString();
            string Error = "";

            int ToBeDeletedId = CreateSomeProduct(Name, out Error);

             Error = "";
            //act 


            bool IsOK = _Service.Delete(ToBeDeletedId, out Error);

            IsOK.Should().BeTrue();
            Error.Should().Be(ProductMessages.SuccessfullyDeleted);




        }




        [Fact]
        public void Create_Should_ReturnError_WhenNameIsRepeatitive()
        {
            string Error = "";
            string Name = "Reapaeted"; 
            CreateSomeProduct(Name, out Error);
            int ActualId = CreateSomeProduct(Name, out Error);

            Error.Should().Be(ProductMessages.RepeatitiveProduct);
            ActualId.Should().Be(0);
            
        }

        public int CreateSomeProduct(string SomeName, out string Error)
        {
            // arrange 
             CreateCommand = new CreateProductCommand
            {
                Name = SomeName,
                CategoryId = 2,
                UnitPrice = 110,

            };
           
            //act 

            int ActualId = _Service.Create(CreateCommand, out Error);
            return ActualId; 
        }



        [Theory]
        [InlineData(2)]
        public void Should_GetProductByIdWhenIdIsInRange(int id)
        {
            string Error = "";

            var Actual = _Service.GetBy(id, out Error);


            Actual.Id.Should().Be(id);
            Error.Should().Be(ProductMessages.SuccessfullGet);
        }

        [Theory]
        [InlineData(1000)]
        public void Should_ReturnNull_WhenCanNotFindOr_IdIsOutOfRange(int id)
        {

            string Error = "";
            var Actual = _Service.GetBy(id, out Error);


            Actual.Should().BeNull();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_returnNull_WhenIdIsNegetiveOrZero(int id)
        {

            string Error = "";
            var Actual = _Service.GetBy(id, out Error);

           // Actual.Id.Should().Be(0);
            Actual.Should().BeNull();

        }



        [Theory]
        [InlineData("SomeUpdatedName")]
        public void Should_Edit_Product(string ExpectedName)
        {
            // arrange 
            string Name = Guid.NewGuid().ToString();
            string Error = "";

            int ActualId = CreateSomeProduct(Name,out Error);

            var EditCommand = new EditProductCommand()
            {
                Id = ActualId,

                // Editting name and price
                CategoryId = CreateCommand.CategoryId,
                UnitPrice = 5, //  new Random().Next(1, 2000),
                Name = ExpectedName,// Guid.NewGuid().ToString(),
            };


            bool IsEdited = _Service.Edit(EditCommand, out Error);



            IsEdited.Should().BeTrue();

            var Actual = _Service.GetBy(ActualId, out Error);

            Actual.Id.Should().Be(EditCommand.Id);
            Actual.Name.Should().Be(EditCommand.Name);


        }



        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Edit_Should_ReturnError_WhenIdIsInvalid(int TestId)
        {
            var Editcommand = new EditProductCommand()
            {
                Id = TestId,
                Name = "SomeUpdatedName",
            };
            string Error = "";

            var Actual = _Service.Edit(Editcommand, out Error);


            Actual.Should().BeFalse();



        }

    }
}
