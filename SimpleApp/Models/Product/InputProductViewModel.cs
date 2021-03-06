using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApp.Models.Product
{
    public class InputProductViewModel
    {
        [MinLength(3)]
        public string Name { get; set; }

        [MinLength(10)]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }
    }
}
