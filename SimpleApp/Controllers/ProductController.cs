using Microsoft.AspNetCore.Mvc;
using SimpleApp.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(InputProductViewModel input)
        {


            return this.Json(input);
        }
    }
}
