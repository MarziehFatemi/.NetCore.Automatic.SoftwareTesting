using Onion.Application.Contracts;
using Onion.Application.Contracts.ProductApplication_Agg;
using Onion.Application.Contracts.ProductCategoryApplicationAgg;
using Presenration.Tests.E2E.Api;
using System;
using TechTalk.SpecFlow;
using TestApi.Controllers;

namespace Presenration.Tests.E2E.StepDefinitions
{
    [Binding]
    public class ProductStepDefinitions
    {
        private readonly ProductCategoryApi _CategoryApi;
        private readonly ProductApi _ProductApi;
        ResultStatus Result = new ResultStatus();
        ProductCategoryViewModel GetCategory = new ProductCategoryViewModel();

       // string CategoryNameForTest = ""; 

        public ProductStepDefinitions()
        {
            _ProductApi = new ProductApi();
            _CategoryApi = new ProductCategoryApi();

        }

        [Given(@"I have already created a product category '([^']*)'")]
        public void GivenIHaveAlreadyCreatedAProductCategory(string name)
        {
            GetCategory = _CategoryApi.GetByName(name).Result;
            GetCategory.Should().NotBe(null);
            GetCategory.Name.Should().Be(name);
            GetCategory.Id.Should().BeGreaterThan(0);

            
        }
        [When(@"I want to create a product with UnitPrice '(.*)' name '([^']*)' in this Category")]
        public void WhenIWantToCreateAProductWithUnitPriceNameInThisCategory(int UnitPrice, string name)
        {
            
            _ProductApi.Command.UnitPrice = UnitPrice;
            _ProductApi.Command.Name = name;
           

        }

        [Then(@"I must see the product category '([^']*)' in the list")]
        public void ThenIMustSeeTheProductCategoryInTheList(string CategoryName)
        {

            var Category = _CategoryApi.GetByName(CategoryName).Result;
            Category.Should().NotBe(null);
            Category.Name.Should().Be(CategoryName);
            Category.Id.Should().Be(GetCategory.Id);

            _ProductApi.Command.CategoryId = GetCategory.Id;

        }

        [Then(@"I can create the product")]
        public void ThenICanCreateTheProduct()
        {
            Result = _ProductApi.CreateProduct().Result;

            Result.Error.Should().Be(ProductMessages.SuccessfullCreation); 
            Result.IsOk.Should().BeTrue();
            Result.Id.Should().BeGreaterThan(0);

        }

        [Then(@"See the product with UnitPrice '(.*)' name '([^']*)' and productCategory '([^']*)' available on the list")]
        public void ThenSeeTheProductWithUnitPriceNameAndProductCategoryAvailableOnTheList(int UnitPrice, string Name, string CategoryName)
        {

            // assert type 1
            //CategoryName = CategoryNameForTest;
            var Actual = _ProductApi.ExactSearch(Name,CategoryName).Result;
            Actual.Should().NotBeNull();
            Actual.Should().BeOfType<List<ProductViewModel>>();
            Actual[0].Id.Should().Be(Result.Id);
            Actual[0].Name.Should().Be(Name);
            Actual[0].Category.Should().Be(CategoryName);
            Actual[0].UnitPrice.Should().Be(UnitPrice);

            ////// tear down 
           var _ResultStatus = _ProductApi.TeadDownProduct(Result.Id).Result;
         
        }

        [Given(@"I have already created a product with UnitPrice '(.*)' and name '([^']*)' in this category")]
        public void GivenIHaveAlreadyCreatedAProductWithUnitPriceAndNameInThisCategory(int UnitPrice, string Name)
        {
            _ProductApi.Command.UnitPrice = UnitPrice;
            _ProductApi.Command.Name = Name;
            _ProductApi.Command.CategoryId = GetCategory.Id;
            Result = _ProductApi.CreateProduct().Result;
            Result.Error.Should().Be(ProductMessages.SuccessfullCreation);
            Result.IsOk.Should().BeTrue();
            Result.Id.Should().BeGreaterThan(0);
        }



        [When(@"I want to create a product with UnitPrice '(.*)' and name '([^']*)' in this category")]
        public void WhenIWantToCreateAProductWithUnitPriceAndNameInThisCategory(int UnitPrice, string Name)
        {
            _ProductApi.Command.UnitPrice = UnitPrice;
            _ProductApi.Command.Name = Name;
            _ProductApi.Command.CategoryId = GetCategory.Id;
            Result = _ProductApi.CreateProduct().Result;
        }



        [Then(@"I should Error Repeatitive Message About It")]
        public void ThenIShouldErrorRepeatitiveMessageAboutIt()
        {
            

            Result.Error.Should().Be(ProductMessages.RepeatitiveProduct);
            Result.IsOk.Should().BeFalse();
            Result.Id.Should().Be(0);
           
        }

        [Then(@"I should not see the product with product category '([^']*)' and name '([^']*)' twice")]
        public void ThenIShouldNotSeeTheProductWithProductCategoryAndNameTwice(string CategoryName, string Name)
        {


            var Actual = _ProductApi.ExactSearch(Name, CategoryName).Result;
            Actual.Should().NotBeNull();
            Actual.Should().BeOfType<List<ProductViewModel>>();
            Actual.Count.Should().Be(1);

            ////// tear down 
            var _ResultStatus = _ProductApi.TeadDownProduct(Actual[0].Id).Result;
            _ResultStatus.IsOk.Should().BeTrue(); 

        }




    }
}
