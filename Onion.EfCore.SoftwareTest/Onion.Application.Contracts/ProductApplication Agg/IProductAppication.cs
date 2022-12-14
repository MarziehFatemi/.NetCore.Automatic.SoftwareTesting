

namespace Onion.Application.Contracts.ProductApplication_Agg
{
    public interface IProductAppication
    {
        EditProductCommand GetBy(int id, out string Error);
        ProductViewModel GetBy(string name);
        List<ProductViewModel> ExactSearch(string name, string Category);
        List<ProductViewModel> GetAll();
        List<ProductViewModel> Search(string name);
        bool Activate(int id, out string Error);
        bool DeActivate(int id, out string Error);
        bool Delete(int id, out string Error); 
        bool Edit(EditProductCommand Command, out string Error);
        int Create(CreateProductCommand Command, out string Error);


    }
}
