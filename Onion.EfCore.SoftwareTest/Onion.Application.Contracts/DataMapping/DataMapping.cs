using Onion.Application.Contracts.ProductApplication_Agg;
using Onion.Domain.Product_agg;
using Onion.Domain.Product_Category_agg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Contracts.DataMapping
{
    public static class DataMapping
    {
       
        public static List<ProductViewModel> ProductList2ProductViewModelList(List<Product> products)
        {
            List<ProductViewModel> productViewModel = new List<ProductViewModel>(); ;

            foreach (var product in products)
            {

                productViewModel.Add(new ProductViewModel(product.Id, product.UintPrice,
                    product.Name,product.IsRemoved, product.Category.Name, product.CreationDate.ToString())); 
             
            }

            return productViewModel;

        }

        public static List<ProductCategoryViewModel> ProdCatList2ProdCatViewList(List<ProductCategory> productCategories)
        {
            List<ProductCategoryViewModel> productCategoryViewModel = new List<ProductCategoryViewModel>(); ;

            foreach (var productCategory in productCategories)
            {

                productCategoryViewModel.Add(new ProductCategoryViewModel()
                {
                    Id = productCategory.Id,
                    Name = productCategory.Name,
                    CreationDate = productCategory.CreationDate.ToString(),

                });


            }

            return productCategoryViewModel;
        }

        public static ProductCategoryViewModel ProdCat2ProdCatView(ProductCategory productCategory)
        {
            ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel()
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                CreationDate = productCategory.CreationDate.ToString(),


            }; 

            return productCategoryViewModel;
        }

        public static ProductViewModel Prod2ProdView(Product product)
        {
            ProductViewModel productViewModel =
            new ProductViewModel(product.Id, product.UintPrice, product.Name, product.IsRemoved,
           product.Category.Name, product.CreationDate.ToString()); 
            

            return productViewModel;
        }

        public static EditProductCommand Product2EditProduct ( Product product)
        {
            return new EditProductCommand(product.Id, product.Name, product.UintPrice, product.CategoryId); 
        }
    }
}
