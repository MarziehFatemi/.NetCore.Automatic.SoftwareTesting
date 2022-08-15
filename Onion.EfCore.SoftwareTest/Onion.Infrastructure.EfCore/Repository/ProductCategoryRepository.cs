using Onion.Domain.Product_Category_agg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool Exist(string categoryName)
        {
            return _context.productCategories.Any(c=>c.Name == categoryName); 
        }

        public ProductCategory Get(int id)
        {
            return _context.productCategories.Find(id); 
                
            //FirstOrDefault(c => c.Id == id);
        }

        public bool Edit (int id, string Name, out string Error)
        {
            var category = Get(id);
            if (category != null)
            {
                category.Edit(Name);
                _context.productCategories.Update(category);
                Error = "با موفقیت ویرایش شد";
                return true;
            }
            else
            {
                Error = " داده مزبور یافت نشد، موجود نیست";
                return false; 
            }

        }
        public bool SaveChanges(out string Error)
        {
            try
            {
                _context.SaveChanges();
                Error = "با موفقیت ذخیره شد";
                return true; 
            }
            catch (Exception ex)
            {
                Error = ex.ToString();
                return false; 

            }
        }

        public List<ProductCategory> Search(string name)
        {
            return _context.productCategories
                .Where(c=> c.Name.Contains(name))
                .OrderBy(c => c.Id)
                .ToList(); 
        }

        public List<ProductCategory> GetAll()
        {
            return _context.productCategories.ToList(); 
        }
    }
}
