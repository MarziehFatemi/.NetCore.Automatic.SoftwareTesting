using RESTFulSense.Clients;
using TestApi.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Onion.Application.Contracts.ProductApplication_Agg;
using Onion.Presentation.WebApi.Controllers;

namespace Presenration.Tests.E2E.Api
{
    public class ProductApi
    {



        private RESTFulApiFactoryClient _restClient;
        public CreateProductCommand Command;


        public ProductApi()
        {
            var applicationFactory = new WebApplicationFactory<Program>();
            var httpClient = applicationFactory.CreateClient();
            _restClient = new RESTFulApiFactoryClient(httpClient);
            Command = new CreateProductCommand();
        }

        public void CreateCommandby(string name, int categoryId, int unitPrice)
        {
            Command = new CreateProductCommand()
            {
                Name = name,
                CategoryId = categoryId,
                UnitPrice = unitPrice,

            };
        }

        public async Task<ResultStatus> CreateProduct()
        {
            // create
            var CreationResult = await _restClient.PostContentAsync<CreateProductCommand, ResultStatus>(ProductConstants.CreatePath, Command);
            return CreationResult;

        }

        public async Task<ResultStatus> TeadDownProduct(int id)
        {
            var _ResultStatus = await _restClient.GetContentAsync<ResultStatus>($"{ProductConstants.RemovePath}/{id}");
            return _ResultStatus;
        }

        public async Task<ProductViewModel> GetByName(string Name)
        {
            var actual = await _restClient.GetContentAsync<ProductViewModel>($"{ProductConstants.GetByNamePath}/{Name}");
            return actual;
        }

        public async Task<List<ProductViewModel>> ExactSearch(string Name,string CategoryName)
        {
            var actual = await _restClient.GetContentAsync<List<ProductViewModel>>($"{ProductConstants.ExactSearchPath}/{Name}/{CategoryName}");
            return actual;
        }


        public async Task<List<ProductViewModel>> GetLast20Product()
        {
            var actual = await _restClient.GetContentAsync<List<ProductViewModel>>(ProductConstants.ListPath);
            return actual;
        }

    }
}
