using Microsoft.EntityFrameworkCore;
using Onion.Domain.Product_agg;
using Onion_Domain.Product_agg.Exceptions;

namespace Onion.Infrastructure.EfCore.Repository
{
    public class ProductRepository : IProductRepository
    {
        Context _context; 
        private readonly string ProductIdIsInvalid = "Product Id Is Invalid";
        public ProductRepository(Context context)
        {
            _context = context;
        }


        public void Create(Product product)
        {
            _context.Add(product); 
        }
        public void Delete (Product product)
        {
            _context.Remove(product);   
        }
        public Product Get(int id)
        {
           
            
            var product = _context.products
                .Include(c => c.Category)
                .FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                throw new ProductIdIsInvalidException(ProductIdIsInvalid); 
            }
               
            return product;
        
        }

        public bool Exist ( string name, int Categoryid)
        {
            return _context.products.Any(c => c.Name == name && c.CategoryId == Categoryid); 
        }
        public void SaveChanges()
        {
                _context.SaveChanges();
               
        }


        public List<Product> Search(string name)
        {
        return _context.products
                .Include(c => c.Category)
                .Where(c => c.Name.Contains(name))
                .OrderBy(c => c.Id)
                .ToList();
         
        }
        public List<Product> GetAll()
        {
            return _context.products
                .Include(c=>c.Category)
                .ToList();
        }

        public void Activate(Product product)
        {
            product.Activate();
            
        }

        public void DeActivate(Product product)
        {
            product.DeActivate();
            
        }
        

        public bool Edit(int id, int UnitPrice, string Name, int CategoryId)
        {
            try
            {
                var Product = Get(id);

                Product.Edit(UnitPrice, Name, CategoryId);
                _context.products.Update(Product);
                
                return true;
            }
            catch 
            {
                throw new ProductIdIsInvalidException(ProductIdIsInvalid);  
                
            }
            

        }
    }
}
