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
            private ProductCategory _Seed = new ProductCategory("Industrial Machine");

        public ProductCategoryServiceTest(RealDatabaseFixture databaseFixture)
        {
            _Repository = new ProductCategoryRepository(databaseFixture.TestContext);

            _Service = new ProductCategoryApplication(_Repository);
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
            public void Should_Create_ProductCategory()
            {
            // arrange 
            var command = new CreateProductCategoryCommand()
            {
                Name = Guid.NewGuid().ToString(),
            };
           
            string Error = "";

            //act 

            int ActualId = _Service.Create(command, out Error);

            ActualId.Should().BeGreaterThan(0);
            Error.Should().Be(ProductCategoryMessages.SuccessfullCreation);

            var ActualEntity = _Service.GetEntity(ActualId,out string Error2);

            ActualEntity.Name = command.Name; 
            ////var Actual = _Service.GetAll();

            ////// assert 
            ////Actual.Should().Contain(Expected);

        }


        [Fact]
        public void Create_Should_ReturnError_WhenNameIsRepeatitive()
        {
            // arrange 
            var command = new CreateProductCategoryCommand()
            {
                Name = _Seed.Name,
            };
            string Error = "";

            //act 

             _Service.Create(command, out Error);

            int ActualId= _Service.Create(command, out Error);

            ActualId.Should().Be(0);
            Error.Should().Be(ProductCategoryMessages.RepeatitiveNameError);

        }




        [Theory]
            [InlineData(2)]
            public void Should_GetProductCategoryByIdWhenIdIsInRange(int id)
            {
            string Error = "";

            var Actual = _Service.GetEntity(id, out Error );


            Actual.Id.Should().Be(id);
            Error.Should().Be(ProductCategoryMessages.SuccessfullGet); 
            }

            [Theory]
            [InlineData(1000)]
            public void Should_ReturnNull_WhenCanNotFindOr_IdIsOutOfRange(int id)
            {

                string Error = "";
                var Actual =  _Service.GetEntity(id, out Error);


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
            var Createcommand = new CreateProductCategoryCommand()
            {
                Name = Guid.NewGuid().ToString(),
            };

            string Error = "";

            int ActualId= _Service.Create(Createcommand, out Error);

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

        }
    
}
