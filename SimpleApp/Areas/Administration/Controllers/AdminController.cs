using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SimpleApp.Models.Product;
using SimpleApp.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApp.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IProductService productService;
        private readonly IMemoryCache memoryCache;

        public AdminController(IProductService productService, IMemoryCache memoryCache)
        {
            this.productService = productService;
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            List<AdminProductViewModel> prds = new List<AdminProductViewModel>();

            var products = productService.All();

            foreach (var prod in products)
            {
                var product = new AdminProductViewModel
                {
                    Id = prod.Id,
                    Name = prod.Name,
                    Description = prod.Description,
                    Price = prod.Price
                };

                prds.Add(product);
            }

            return this.View(prds);
        }

        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await this.productService.DeleteAsync(id);

            return this.Redirect("/Administration/Admin");
        }
    }
}
