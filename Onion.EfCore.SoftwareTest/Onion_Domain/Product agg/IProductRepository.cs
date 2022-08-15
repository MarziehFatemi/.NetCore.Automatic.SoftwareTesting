using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Domain.Product_agg
{
    public interface IProductRepository
    {
        Product Get(int id, out bool IsNull, out string Error);
        List<Product> GetAll();
        void Create(Product product);
        bool Edit (int id, int UnitPrice, string Name, int CategoryId, out string Error);
        void Activate(Product product);
        void DeActivate(Product product);
        bool Exist (string name, int categoryId);
        bool SaveChanges(out string Error);
        List<Product> Search(string name);


    }
}
