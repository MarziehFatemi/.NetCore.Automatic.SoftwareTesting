using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Contracts.ProductApplication_Agg
{
    public class CreateProductCommand
    {
        [DisplayName("قیمت")]
        [Required(ErrorMessage = "قیمت اجباری است")]
        [JsonProperty(PropertyName = "unitPrice")]
        public int UnitPrice { get;  set; }

        [DisplayName("نام")]
        [Required(ErrorMessage = "نام اجباری است")]
        [MaxLength(255)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get;  set; }

        [DisplayName("گروه محصول")]
        [Required(ErrorMessage = "گروه محصول اجباری است")]
        [Range(1,100,ErrorMessage ="گروه محصول را درست انتخاب کنید محدودیت 100 محصول")]
        [JsonProperty(PropertyName = "categoryId")]
        public int CategoryId{ get; set; }
       
    }

}
