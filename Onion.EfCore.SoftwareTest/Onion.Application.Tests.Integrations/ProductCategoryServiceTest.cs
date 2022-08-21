using FluentAssertions;
using Onion.Application.Contracts;
using Onion.Application.Contracts.ProductCategoryApplicationAgg;
using Onion.Domain.Product_Category_agg;
using Onion.Infrastructure.EfCore.Repository;
using Onion.Infrastructure.Tests.Integratioins;

namespace Onion.Application.Tests.Integrations
{
    public class ProductCategoryServiceTest : IClassFixture<RealDatabaseFixture>
    {

            private readonly ProductCategoryRepository _Repository;
            private readonly ProductCategoryApplication _Service;
           // private ProductCategory _Seed = new ProductCategory("Industrial Machine");

        public ProductCategoryServiceTest(RealDatabaseFixture databaseFixture)
        {
            _Repository = new ProductCategoryRepository(databaseFixture.TestContext);

            _Service = new ProductCategoryApplication(_Repository);
        }



            [Fact]
            public void Should_Return_All_ProductCategories()
            {
            // arrange
            string name = Guid.NewGuid().ToString();
            string Error = "";

            // act 
            int ActualId = CreateSomeProductCategory(name, out Error);

            // assert

            // act 
            var Actual = _Service.GetAll();

            // assert 
            Actual.Should().HaveCountGreaterThanOrEqualTo(1);

            // teardown 
            _Service.Delete(ActualId, out Error);

        }


            [Fact]
            public void Should_Create_ProductCategory()
            {
            // arrange
            string name = Guid.NewGuid().ToString();
            string Error = "";

            // act 
            int ActualId = CreateSomeProductCategory(name, out Error); 
            
            // assert
            
            ActualId.Should().BeGreaterThan(0);
            Error.Should().Be(ProductCategoryMessages.SuccessfullCreation);

            var ActualEntity = _Service.GetEntity(ActualId,out string Error2);

            ActualEntity.Name = name;


            // teardown  no tear down for seed 
           // _Service.Delete(ActualId, out Error);


        }


        [Fact]
        public void Should_Delete_ProductCategory()
        {
            // arrange
            string name = Guid.NewGuid().ToString();
            string Error = "";

            // act 
            int ActualId = CreateSomeProductCategory(name, out Error);


            //act 


            bool IsOK =  _Service.Delete(ActualId, out Error); 
           
            IsOK.Should().BeTrue();
            Error.Should().Be(ProductCategoryMessages.SuccessfullyDeleted);




        }

        [Fact]
        public void Create_Should_ReturnError_WhenNameIsRepeatitive()
        {
            // arrange
            string name = Guid.NewGuid().ToString();
            string Error = "";
            int ActualId0 = CreateSomeProductCategory(name, out Error);

            // act 
            int ActualId = CreateSomeProductCategory(name, out Error);


            ActualId.Should().Be(0);
            Error.Should().Be(ProductCategoryMessages.RepeatitiveNameError);

            // teardown 
            _Service.Delete(ActualId0, out Error);

        }



        [Fact]
            public void Should_GetProductCategoryByIdWhenIdIsInRange()
            {
            // arrange
            string name = Guid.NewGuid().ToString();
            string Error = "";
            int ActualId = CreateSomeProductCategory(name, out Error);


             Error = "";
            // Act

            var Actual = _Service.GetEntity(ActualId, out Error );


            Actual.Id.Should().Be(ActualId);
            Error.Should().Be(ProductCategoryMessages.SuccessfullGet);

            // teardown 
            _Service.Delete(ActualId, out Error);

        }

            [Fact]
            public void Should_ReturnNull_WhenCanNotFindOr_IdIsOutOfRange()
            {
            // arrange
            string name = Guid.NewGuid().ToString();
            string Error = "";
            int ActualId = CreateSomeProductCategory(name, out Error);
            // teardown 
            _Service.Delete(ActualId, out Error);


            Error = "";
                var Actual =  _Service.GetEntity(ActualId, out Error);


                Actual.Should().BeNull();
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            public void Should_returnNull_WhenIdIsNegetiveOrZero(int id)
            {

            string Error = "";
            var Actual = _Service.GetEntity(id, out Error);


            Actual.Should().BeNull();

        }



            [Theory]
            [InlineData("SomeUpdatedName")]
            public void Should_Edit_ProductCategory(string ExpectedName)
            {
            // arrange 
            // arrange
            string name = Guid.NewGuid().ToString();
            string Error = "";
            int ActualId = CreateSomeProductCategory(name, out Error);


            var Editcommand = new EditProductCategoryCommand()
            {
                Id = ActualId,
                Name = "SomeNewName",
            };


            bool IsEdited = _Service.Edit(Editcommand,out Error);



            IsEdited.Should().BeTrue();

           var Actual = _Service.GetEntity(ActualId, out Error);

            Actual.Id.Should().Be(Editcommand.Id);
            Actual.Name.Should().Be(Editcommand.Name);

            // teardown 
            _Service.Delete(ActualId, out Error);

        }



            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            public void Edit_Should_ReturnError_WhenIdIsInvalid(int TestId)
            {
            var Editcommand = new EditProductCategoryCommand()
            {
                Id = TestId,
                Name = "SomeUpdatedName",
            };
            string Error = "";

            var Actual = _Service.Edit(Editcommand, out Error);


            Actual.Should().BeFalse();


            

        }


        public int CreateSomeProductCategory(string name, out string Error )
        {
            // arrange 
            var command = new CreateProductCategoryCommand()
            {
                Name = name, 
            };

            //act 

            int ActualId = _Service.Create(command, out Error);
             return ActualId; 


        }

    }
    
}
