using Microsoft.AspNetCore.Mvc.Testing;
using Onion.Application.Contracts;
using Onion_Domain.Product_Category_agg;
using RESTFulSense.Clients;
using TestApi.Controllers;

namespace Presenration.Tests.E2E.Api
{
    public class ProductCategoryApi
    {
       


        private RESTFulApiFactoryClient _restClient;
        CreateProductCategoryCommand Command;


        public ProductCategoryApi()
        {
            var applicationFactory = new WebApplicationFactory<Program>();
            var httpClient = applicationFactory.CreateClient();
            _restClient = new RESTFulApiFactoryClient(httpClient);
            Command = new CreateProductCategoryCommand();
        }

        public void CreateCommandby(string name)
        {
            Command = new CreateProductCategoryCommand(name);
        }

        public async Task<ResultStatus> CreateProductCategory()
        {
            // create
            var CreationResult = await _restClient.PostContentAsync<CreateProductCategoryCommand, ResultStatus>(ProductCategoryConstants.CreatePath, Command);
            return CreationResult;
            
        }

        public async Task<ResultStatus> TeadDownProductCategory(int id)
        {
            var _ResultStatus = await _restClient.GetContentAsync<ResultStatus>($"{ProductCategoryConstants.RemovePath}/{id}");
            return _ResultStatus;
        }

        public async Task<ProductCategoryViewModel> GetByName(string Name)
        {
            var actual = await _restClient.GetContentAsync<ProductCategoryViewModel>($"{ProductCategoryConstants.GetByNamePath}/{Name}");
            return actual;
        }

        public async Task<List<ProductCategoryViewModel>> ExactSearch(string Name)
        {
            var actual = await _restClient.GetContentAsync<List<ProductCategoryViewModel>>($"{ProductCategoryConstants.ExactSearchPath}/{Name}");
            return actual;
        }


        public async Task<List<ProductCategoryViewModel>> GetLast20ProductCategory()
        {
            var actual = await _restClient.GetContentAsync<List<ProductCategoryViewModel>>(ProductCategoryConstants.ListPath);
            return actual; 
        }
    }
       
}
