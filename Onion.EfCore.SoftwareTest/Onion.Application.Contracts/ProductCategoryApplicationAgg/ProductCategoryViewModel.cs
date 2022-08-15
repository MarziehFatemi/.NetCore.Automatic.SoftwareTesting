using Newtonsoft.Json;
using Onion.Domain.Product_Category_agg;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Onion.Application.Contracts
{

    public class ProductCategoryViewModel
    {
        [DisplayName("شماره")]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [DisplayName("نام")]
        [Required(ErrorMessage = "نام اجباری است")]
        [MaxLength(255)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [DisplayName("تاریخ ایجاد")]
        [JsonProperty(PropertyName = "creationDate")]
        public string CreationDate { get; set; }

        public ProductCategoryViewModel()
        {

        }
        public ProductCategoryViewModel(int id, string name, DateTime creationDate)
        {
            Id = id;
            Name = name;
           CreationDate = creationDate.ToString(); 
        }


        
    }
    
}
