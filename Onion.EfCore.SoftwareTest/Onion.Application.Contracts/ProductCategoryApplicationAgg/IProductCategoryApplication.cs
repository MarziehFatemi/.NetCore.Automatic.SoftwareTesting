using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Contracts
{
    public interface IProductCategoryApplication
    {
       
        int Create(CreateProductCategoryCommand Command, out string Error);
        bool Edit (EditProductCategoryCommand Command, out string Error);
         List<ProductCategoryViewModel> Search(string name);
        List<ProductCategoryViewModel> GetAll();
        EditProductCategoryCommand GetEntity(int Id); 

    }
}
