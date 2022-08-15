using Onion.Application.Contracts;
using Onion.Application.Contracts.DataMapping;
using Onion.Application.Contracts.ProductApplication_Agg;
using Onion.Domain.Product_agg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application
{
    public class ProductApplication : IProductAppication
    {
        private readonly IProductRepository _IProductRepository; 
        public ProductApplication(IProductRepository iProductRepository)
        {
            _IProductRepository = iProductRepository; 

        }
        public bool Activate(int id, out string Error)
        {
           
            try
            {
                var product = _IProductRepository.Get(id);

                _IProductRepository.Activate(product);
                
                _IProductRepository.SaveChanges();
                Error = "با موفقیت انجام شد";
                return true; 
            }
            catch (Exception ex)
            {
                
                Error = ex.Message.ToString();
                return false; 

            }


        }
        public bool DeActivate(int id, out string Error)
        {
            
            try
            {
                var product = _IProductRepository.Get(id);

                _IProductRepository.DeActivate(product);
                 _IProductRepository.SaveChanges();
                Error = "با موفقیت انجام شد";
                return true; 
            }
            catch (Exception e)
            {
                Error = e.Message.ToString(); 
                return false; 

            }

        }

        public int Create(CreateProductCommand Command, out string Error)
        {
            if (!_IProductRepository.Exist(Command.Name, Command.CategoryId))
            {
                var Product = new Product(Command.UnitPrice, Command.Name, Command.CategoryId);

                try
                {
                    _IProductRepository.Create(Product);
                    _IProductRepository.SaveChanges();
                    Error = "با موفقیت انجام شد";
                  return Product.Id;
                }
                catch(Exception e)
               
                {
                    Error = e.Message.ToString(); 
                    return 0;
                }
            }
            {
                Error = ProductMessages.RepeatitiveProduct; 
                return 0; 
            }

        }



        public bool Edit(EditProductCommand Command, out string Error)
        {
            
            try
            {
                _IProductRepository.Edit(Command.Id, Command.UnitPrice, Command.Name, Command.CategoryId);
            
                _IProductRepository.SaveChanges();
                Error = "با موفقیت انجام شد";
                return true;
            }
            catch (Exception e )
            {
                Error = e.Message.ToString(); 
                return false; 
            }

        }
        public List<ProductViewModel> Search(string name)
        {
            List<Product> products = _IProductRepository.Search(name); 
            return DataMapping.ProductList2ProductViewModelList(products);


        }

        public List<ProductViewModel> GetAll()
        {
            List<Product> products = _IProductRepository.GetAll(); 
          return DataMapping.ProductList2ProductViewModelList(products); 
        }

        public EditProductCommand GetBy(int id,out bool IsNull, out string Error)
        {
            try
            {
                var product = _IProductRepository.Get(id);
                IsNull = false;
                Error = "OK";
                return DataMapping.Product2EditProduct(product);

            }

            catch (Exception ex)
            {
                Error = ex.Message.ToString();
                IsNull = true; 
                return new EditProductCommand();
            }
            
            
        }
    }
}
