using Onion.Application.Contracts;

using TestApi.Controllers; 
using System;
using TechTalk.SpecFlow;
using Onion.Application.Contracts.ProductCategoryApplicationAgg;
using Presenration.Tests.E2E.Api;

namespace Presenration.Tests.E2E.StepDefinitions
{
    [Binding]
    public class ProductCategoryStepDefinitions
    {
        private readonly ProductCategoryApi _ProductCategoryApi;
        ResultStatus Result  = new ResultStatus();
        ResultStatus FirstCreationResult = new ResultStatus();

        public ProductCategoryStepDefinitions()
        {
            _ProductCategoryApi = new ProductCategoryApi();
           
        }
        


        [Given(@"I Want To Create the Product Category '([^']*)'")]
        public void GivenIWantToCreateTheProductCategory(string name)
        {
            //name = Guid.NewGuid().ToString();
            _ProductCategoryApi.CreateCommandby(name); 
        }

        [When(@"I Press Add Button")]
        public void WhenIPressAddButton()
        {
            Result = _ProductCategoryApi.CreateProductCategory().Result;
       
        }

        [Then(@"I can see '([^']*)' gets Available On the List")]
        public  void ThenICanSeeGetsAvailableOnTheList(string name)
        {
            // assert type 1
           var CreatedEntity = _ProductCategoryApi.GetByName(name).Result;
           CreatedEntity.Should().NotBe(null);
           CreatedEntity.Name.Should().Be(name);
            CreatedEntity.Id.Should().Be(Result.Id);


            // assert type 2
            var Actual = _ProductCategoryApi.GetLast20ProductCategory().Result;

            Actual[0].Name.Should().Be(CreatedEntity.Name);
            Actual[0].Id.Should().Be(CreatedEntity.Id);
            Actual[0].CreationDate.Should().Be(CreatedEntity.CreationDate);


            // tear down 
            _ProductCategoryApi.TeadDownProductCategory(CreatedEntity.Id);


        }


        [Given(@"I Have Already Created PrdocutCategory '([^']*)'")]
        public void GivenIHaveAlreadyCreatedPrdocutCategory(string name)
        {
            _ProductCategoryApi.CreateCommandby(name);
           FirstCreationResult = _ProductCategoryApi.CreateProductCategory().Result;
        }

        [When(@"I Try To Create It Again")]
        public async void WhenITryToCreateItAgain()
        {
            Result = _ProductCategoryApi.CreateProductCategory().Result;

        }

        [Then(@"'([^']*)' Should Not Be Appeared In List Twice")]
        public void ThenShouldNotBeAppearedInListTwice(string name)
        {
            var Actual = _ProductCategoryApi.ExactSearch(name).Result;
            //assert
            Actual.Should().BeOfType<List<ProductCategoryViewModel>>();
            Actual.Count.Should().Be(1);
            Actual[0].Id.Should().Be(FirstCreationResult.Id);
            Actual[0].Name.Should().Be(name);


            // tear down 
            _ProductCategoryApi.TeadDownProductCategory(FirstCreationResult.Id);



        }

        [Then(@"I should Receive According Message About It")]
        public void ThenIShouldReceiveAccordingMessageAboutIt()
        {
            Result.Error.Should().Be(ProductCategoryMessages.RepeatitiveNameError);
            Result.Id.Should().Be(0);
            Result.IsOk.Should().BeFalse();

        }
    }
}
