using Newtonsoft.Json;


namespace Onion.Application.Contracts
{
    public  class EditProductCommand: CreateProductCommand
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get;  set; }
        public EditProductCommand()
        {

        }
        public EditProductCommand(int id, string name, int unitPrice, int categoryId)
        {

            Id = id;
            Name = name;
            UnitPrice = unitPrice;
            CategoryId = categoryId; 
        }
    }
}
