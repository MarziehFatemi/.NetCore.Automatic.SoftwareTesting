using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion_Domain.Product_agg.Exceptions
{
    public class ProdcutIsRepetitiveException: Exception
    {
       
        
        public ProdcutIsRepetitiveException(string message)
        : base(message) { }

    }
}
