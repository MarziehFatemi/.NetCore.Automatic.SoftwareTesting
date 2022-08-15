using Onion.Domain.Product_Category_agg;
using System;

namespace Onion.Domain.Product_agg
{
    public  class Product
    {

        public int Id { get; private set; }
        public int UintPrice { get; private set; }
        public string Name { get; private set; }
        public bool IsRemoved { get; private set; }
        public int CategoryId { get; private set; }
        public DateTime CreationDate { get; private set; }


        public ProductCategory Category { get; private set; }

        public Product(int uintPrice, string name,  int categoryId)
        {
           
            UintPrice = uintPrice;
            Name = name;
            IsRemoved = false; 
            CategoryId = categoryId;
            CreationDate= DateTime.Now;


        }
  
        public void Activate ()
        {
            IsRemoved = false; 
        }
        public void DeActivate()
        {
            IsRemoved = true;
        }

        public void Edit(int uintPrice, string name, int categoryId)
        {

            UintPrice = uintPrice;
            Name = name;
            CategoryId = categoryId;

        }

    }
}
