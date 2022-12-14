
using System.Collections.Generic;

namespace Onion.Domain.Product_Category_agg
{
    public interface IProductCategoryRepository
    {
        ProductCategory Get(int id);
        List<ProductCategory> GetAll();
        bool Exist(string categoryName);
        void Create(ProductCategory productCategory);
        void Delete(ProductCategory productCategory);
        bool Edit(int id, string Name); 
        void SaveChanges();
        ProductCategory GetBy(string name);
        List<ProductCategory> ExactSearch(string name);
        // List<ProductCategory> Search(string name);

    }
}
