using Newtonsoft.Json;
using Onion.Domain.Product_agg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Onion.Application.Contracts.ProductApplication_Agg
{
    public class ProductViewModel
    {
        [DisplayName("شماره")]
        [JsonProperty(PropertyName = "id")]
        public int Id { get;  set; }

        [DisplayName("قیمت")]
        [Required(ErrorMessage = "قیمت اجباری است")]
        [MaxLength(255)]
        [JsonProperty(PropertyName = "unitPrice")]
        public int UnitPrice { get;  set; }


        [DisplayName("نام")]
        [Required(ErrorMessage = "نام اجباری است")]
        [MaxLength(255)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get;  set; }

        [JsonProperty(PropertyName = "isRemoved")]
        public bool IsRemoved { get;  set; }

        [DisplayName("گروه محصول")]
        [Required(ErrorMessage = "گروه محصول اجباری است")]
        [MaxLength(255)]
        [JsonProperty(PropertyName = "category")]
        public string Category { get;  set; }

        [DisplayName("تاریخ ایجاد")]
        [JsonProperty(PropertyName = "creationDate")]
        public string CreationDate { get;  set; }

        public ProductViewModel(int id, int uintPrice, string name, bool isRemoved, string category, string creationDate)
        {
            Id = id;
            UnitPrice = uintPrice;
            Name = name;
            IsRemoved = isRemoved;
            Category = category;
            CreationDate = creationDate;
        }

        

    }
}
