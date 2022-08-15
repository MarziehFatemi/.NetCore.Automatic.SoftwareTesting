﻿using Onion.Application.Contracts;
using Onion.Application.Contracts.DataMapping;
using Onion.Application.Contracts.ProductCategoryApplicationAgg;
using Onion.Domain.Product_Category_agg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository ProductCatecoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCatecoryRepository)
        {
            ProductCatecoryRepository = productCatecoryRepository;
        }


        public int Create(CreateProductCategoryCommand Command, out string Error)
        {
            if (ProductCatecoryRepository.Exist(Command.Name))
            {
                Error = ProductCategoryMessages.RepeatitiveNameError; 

                return 0;
            }
            else
            {
                var PCategory = new ProductCategory(Command.Name);
                try
                { 
                ProductCatecoryRepository.Create(PCategory);
                    ProductCatecoryRepository.SaveChanges();
                
                    Error = "با موفقیت ذخیره شد";
                    return PCategory.Id;
                }
                catch (Exception ex)
                {
                    Error = ex.Message.ToString(); 

                    return 0;
                }
            }
        }

        public bool Edit(EditProductCategoryCommand Command, out string Error)
        {
            Error = "";

            try
            {

                ProductCatecoryRepository.Edit(Command.Id, Command.Name);

                ProductCatecoryRepository.SaveChanges();

                Error = "با موفقیت انجام شد";
                return true;
            }
             catch (Exception e)  
                {
                Error = e.Message.ToString();
                    return false;
                }
           
           


        }

        ////public List<ProductCategoryViewModel> Search(string name)
        ////{
        ////    return DataMapping.ProdCatList2ProdCatViewList(ProductCatecoryRepository.Search(name));


        ////}

        public List<ProductCategoryViewModel> GetAll()
        {
            return DataMapping.ProdCatList2ProdCatViewList(ProductCatecoryRepository.GetAll());


        }


        public EditProductCategoryCommand GetEntity(int Id)
        {
           
                var productCategory = ProductCatecoryRepository.Get(Id);


                var Command = new EditProductCategoryCommand(Id, productCategory.Name);
            
                return Command;
        }
       

    }
}
