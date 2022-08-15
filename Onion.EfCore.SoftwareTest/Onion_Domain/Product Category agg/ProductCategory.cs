using Onion.Domain.Product_agg;
using System;
using System.Collections.Generic;

namespace Onion.Domain.Product_Category_agg
{
    public class ProductCategory
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime CreationDate { get; private set; }


        public List<Product> Products { get; private set; }
        
        public ProductCategory( string name)
        {
            
            Name = name;
            CreationDate = DateTime.Now;
        }
        public void Edit (string name)
        {
            Name = name; 
        }
    }
}
