using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache memoryCache;

        public ProductController(IProductService productService, IMemoryCache memoryCache)
        {
            this.productService = productService;
            this.memoryCache = memoryCache;
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
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

        public IActionResult All([FromRoute] string name)
        {
            List<OutputProductViewModel> prds;

            if (!memoryCache.TryGetValue("prds", out prds))
            {
                memoryCache.Set("prds", productService.All<OutputProductViewModel>());
            }

            prds = memoryCache.Get("prds") as List<OutputProductViewModel>;
           
         
            return this.View(prds);
        }
    }
}
