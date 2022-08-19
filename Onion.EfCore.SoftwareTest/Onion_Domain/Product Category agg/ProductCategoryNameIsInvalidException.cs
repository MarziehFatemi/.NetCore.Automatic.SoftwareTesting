using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion_Domain.Product_Category_agg
{
    public class ProductCategoryNameIsInvalidException : Exception
    {
        public ProductCategoryNameIsInvalidException(string? message) : base(message)
        {
        }
    }
}
