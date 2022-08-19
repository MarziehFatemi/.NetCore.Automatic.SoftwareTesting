﻿using Onion.Domain.Product_Category_agg;
using Onion_Domain.Product_agg.Exceptions;


namespace Onion.Infrastructure.EfCore.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        Context _context;
        public ProductCategoryRepository(Context context)
        {
            _context = context;
        }

        public void Create(ProductCategory productCategory)
        {
            _context.Add(productCategory); 
        }
        public void Delete(ProductCategory productCategory)
        {
            _context.Remove(productCategory);
        }

        public bool Exist(string categoryName)
        {
            return _context.productCategories.Any(c=>c.Name == categoryName); 
        }

        public ProductCategory Get(int id)
        {
            if (id <= 0)
                throw new ProductIdIsInvalidException();
            else
            {
                var productCategory = _context.productCategories.Find(id);
                if (productCategory == null)
                    throw new ProductIdIsInvalidException();

                return productCategory;
            }
          
        }

        public bool Edit (int id, string Name)
        {
            try
            {
                var category = Get(id);
                category.Edit(Name);
                _context.productCategories.Update(category);
                return true;
            }
            catch
            {
                throw new ProductIdIsInvalidException();
            }

            

        }
        public void SaveChanges()
        {
            
                _context.SaveChanges();
              
        }

        ////public List<ProductCategory> Search(string name)
        ////{
        ////    return _context.productCategories
        ////        .Where(c=> c.Name.Contains(name))
        ////        .OrderBy(c => c.Id)
        ////        .ToList(); 
        ////}

        public List<ProductCategory> GetAll()
        {
            return _context.productCategories.ToList(); 
        }
    }
}
