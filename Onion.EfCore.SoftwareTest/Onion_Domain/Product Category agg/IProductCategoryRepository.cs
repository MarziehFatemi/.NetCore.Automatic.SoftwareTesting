
using System.Collections.Generic;

namespace Onion.Domain.Product_Category_agg
{
    public interface IProductCategoryRepository
    {
        ProductCategory Get(int id);
        List<ProductCategory> GetAll();
        bool Exist(string categoryName);
        void Create(ProductCategory productCategory);
        bool Edit(int id, string Name, out string Error); 
        bool SaveChanges(out string Error);
        List<ProductCategory> Search(string name);

    }
}
