using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Contracts
{
    public class EditProductCategoryCommand : CreateProductCategoryCommand
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        public EditProductCategoryCommand()
        {

        }
        public EditProductCategoryCommand(int id, string name)
        {
            Id = id;
            Name = name; 
        }
    }
}
