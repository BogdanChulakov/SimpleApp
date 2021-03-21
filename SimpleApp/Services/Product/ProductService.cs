using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleApp.Data;
using SimpleApp.Data.Models;
using SimpleApp.Models.Product;

namespace SimpleApp.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(string name, string description, double price)
        {
            var product = new Data.Models.Product
            {
                Name = name,
                Description = description,
                Price = price
            };

            await this.dbContext.Products.AddAsync(product);
            await this.dbContext.SaveChangesAsync();
        }

        public List<OutputProductViewModel> All<T>()
        {
            var prds = new List<OutputProductViewModel>();
            var products = this.dbContext.Products.ToList();
            foreach (var prod in products)
            {
                var product = new OutputProductViewModel
                {
                    Name = prod.Name,
                    Description = prod.Description,
                    Price = prod.Price
                };

                prds.Add(product);
            }

            return prds;
        }

    }
}
