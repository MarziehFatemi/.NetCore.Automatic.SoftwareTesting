using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Onion.Application.Contracts
{
    public class CreateProductCategoryCommand

    {
        [DisplayName("نام")]
        [Required(ErrorMessage = "نام اجباری است")]
        [MaxLength(255)]
        [JsonProperty(PropertyName = "name")]
        public string  Name { get; set; }

        public CreateProductCategoryCommand(string name)
        {
            Name = name;
        }

        public CreateProductCategoryCommand()
        {
        }
    }

   
    
}
