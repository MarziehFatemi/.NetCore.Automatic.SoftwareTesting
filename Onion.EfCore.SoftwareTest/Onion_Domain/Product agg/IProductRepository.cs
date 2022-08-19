using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Domain.Product_agg
{
    public interface IProductRepository
    {
        Product Get(int id);
        List<Product> GetAll();
        void Create(Product product);
        void Delete(Product product);
        bool Edit (int id, int UnitPrice, string Name, int CategoryId);
        void Activate(Product product);
        void DeActivate(Product product);
        bool Exist (string name, int categoryId);
        void SaveChanges();
        List<Product> Search(string name);


    }
}
