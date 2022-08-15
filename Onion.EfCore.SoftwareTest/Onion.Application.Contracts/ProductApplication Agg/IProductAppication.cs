

namespace Onion.Application.Contracts.ProductApplication_Agg
{
    public interface IProductAppication
    {
        EditProductCommand GetBy(int id, out bool IsNull, out string Error);
        List<ProductViewModel> GetAll();
        List<ProductViewModel> Search(string name);
        bool Activate(int id, out string Error);
        bool DeActivate(int id, out string Error);
        bool Edit(EditProductCommand Command, out string Error);
        int Create(CreateProductCommand Command, out string Error);


    }
}
