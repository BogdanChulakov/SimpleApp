using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SimpleApp.Models.Product;
using SimpleApp.Services.Product;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IMemoryCache memoryCache;
        private readonly IMapper mapper;

        public ProductController(IProductService productService, IMemoryCache memoryCache,IMapper mapper)
        {
            this.productService = productService;
            this.memoryCache = memoryCache;
            this.mapper = mapper;
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

            this.memoryCache.Remove("prds");

            this.TempData["Message"] = "Product was added successfully!";

            return this.Redirect("All");
        }

        public IActionResult All(string sort)
        {
            List<OutputProductViewModel> prds;

            if (!memoryCache.TryGetValue("prds", out prds))
            {

                var products = productService.All();

                prds = new List<OutputProductViewModel>();

                foreach (var prod in products)
                {
                    var product = mapper.Map<OutputProductViewModel>(prod);              
                    prds.Add(product);
                }
                memoryCache.Set("prds", prds);
            }

            prds = memoryCache.Get("prds") as List<OutputProductViewModel>;

            switch (sort)
            {
                case "priceA":
                    prds = prds.OrderBy(x => x.Price).ToList();
                    break;
                case "priceD":
                    prds = prds.OrderByDescending(x => x.Price).ToList();
                    break;
                case "nameD":
                    prds = prds.OrderByDescending(x => x.Name).ToList();
                    break;
                case "nameA":
                    prds = prds.OrderBy(x => x.Name).ToList();
                    break;
                default:
                    break;
            }

            return this.View(prds);
        }
    }
}
