using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApp.Models.Product
{
    public class AllProductsViewModel
    {
        public IEnumerable<OutputProductViewModel> Products { get; set; }
    }
}
