using Microsoft.AspNetCore.Mvc;
using SimpleApp.Data.Models;
using SimpleApp.Models.Product;
using SimpleApp.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(InputProductViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            await this.productService.AddAsync(input.Name, input.Description, input.Price);

            this.TempData["Message"] = "Product was added successfully!";

            return this.Redirect("All");
        }

        public IActionResult All() 
        {
            var products = this.productService.All<OutputProductViewModel>();

            var prds = new AllProductsViewModel();
            prds.Products = products;
            return this.View(prds);
        }
    }
}
