using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SimpleApp.Models.Product;
using SimpleApp.Services.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleApp.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IProductService productService;
        private readonly IMemoryCache memoryCache;
        private readonly IMapper mapper;

        public AdminController(IProductService productService, IMemoryCache memoryCache,IMapper mapper)
        {
            this.productService = productService;
            this.memoryCache = memoryCache;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            List<AdminProductViewModel> prds = new List<AdminProductViewModel>();

            var products = productService.All();

            foreach (var prod in products)
            {
                var product = mapper.Map<AdminProductViewModel>(prod);

                prds.Add(product);
            }

            return this.View(prds);
        }

        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await this.productService.DeleteAsync(id);

            this.memoryCache.Remove("prds");

            return this.Redirect("/Administration/Admin");
        }
    }
}
