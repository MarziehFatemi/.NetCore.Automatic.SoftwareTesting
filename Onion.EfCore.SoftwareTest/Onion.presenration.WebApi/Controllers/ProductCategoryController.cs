using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Application.Contracts;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        
        public ResultStatus _ResultStatus = new ResultStatus(); 

        private readonly IProductCategoryApplication _IProductCategoryApplication;

        public ProductCategoryController(IProductCategoryApplication iProductCategoryApplication)
        {
            _IProductCategoryApplication = iProductCategoryApplication;
        }
        
        [HttpGet]
        public List<ProductCategoryViewModel> GetProductCategoryList()
        {
            return _IProductCategoryApplication.GetAll();
        }

        [HttpGet("{id}")]
        public EditProductCategoryCommand GetProductCategory(int id)
        {
            return _IProductCategoryApplication.GetEntity(id);

        }
        [HttpPost("EditProdcutCategory")]
        public ResultStatus PostEditProdcutCategory(EditProductCategoryCommand Command)
        {

            _ResultStatus.IsOk = _IProductCategoryApplication.Edit(Command, out string Error);
            _ResultStatus.Error = Error;
            return _ResultStatus;
        }

        [HttpPost("CreateProductCategory")]
        public ResultStatus PostCreateProductCategory(CreateProductCategoryCommand Command)
        {
            string Error = "";
           // if (ModelState.IsValid)
            //{
                _ResultStatus.Id= _IProductCategoryApplication.Create(Command, out Error);
                _ResultStatus.Error = Error;
            if (_ResultStatus.Id != 0)
                _ResultStatus.IsOk = true;
            //}
            ////else
            ////{
            ////    _ResultStatus.Error = ModelState.Select(x => x.Value.Errors)
            ////               .Where(y => y.Count > 0).ToString();
            ////    _ResultStatus.IsOk = false;


                ////}
            return _ResultStatus;


        }
    }
}
