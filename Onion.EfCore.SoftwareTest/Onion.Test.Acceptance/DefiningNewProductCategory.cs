using Onion.Application.Contracts;
using TestStack.BDDfy;
using Newtonsoft.Json;
using RestSharp;

namespace Onion.Test.Acceptance
{
    public class DefiningNewProductCategory : IClassFixture<StartHostFixture>
    {



        [Fact]
        public void CreatingANewProductCategory()
        {
            string TestName = Guid.NewGuid().ToString();
            var Command = new CreateProductCategoryCommand(TestName); 

                 this.Given(_ => _.IWantToCreateTheFollowingProductCategory(Command), "Given I Want To Create WebApi As A Course")
                .When(_ => _.IPressAddButton())
                .Then(_ => _.TheFollowingProductCategoryShouldBeAvailableOnList(Command),
                    "Then It Should Be Available On List")
                .BDDfy();
        }

        ////[Fact]
        ////public void DuplicatedProductCategoryCantBeCreated()
        ////{
        ////    var ProductCategory = CreateSomeProductCategory();

        ////    this.Given(_ => _.IHaveAlreadyCreatedFollowingProductCategory(ProductCategory))
        ////        .When(_ => _.ITryToCreateItAgain())
        ////        .Then(_ => _.TheProductCategoryShouldNotBeAppearedInListTwice())
        ////        .BDDfy();
        ////}





        public  CreateProductCategoryCommand _ProductCategory;

        public  void IWantToCreateTheFollowingProductCategory(CreateProductCategoryCommand Command)
        {
            _ProductCategory = Command;
        }

        public  void IPressAddButton()
        {
            var id = PostTheProductCategory();

        }


        public  void TheFollowingProductCategoryShouldBeAvailableOnList(CreateProductCategoryCommand _ProductCategory)
        {
        }


        private  int PostTheProductCategory()
        {
            var restClient = new RestClient(HostConstants.Endpoint + HostConstants.ProductCategoryCreatePath);
            var restRequest = new RestRequest().AddJsonBody(_ProductCategory);
            //restRequest.Method = Method.POST;
            var response = restClient.Post(restRequest);
            var id = JsonConvert.DeserializeObject<int>(response.Content);
            return id;
        }




    }
}