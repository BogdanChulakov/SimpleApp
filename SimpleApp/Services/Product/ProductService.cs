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

        public List<Data.Models.Product> All()
        {
         
            var products = this.dbContext.Products.ToList();

            return products;
        }

        public async Task DeleteAsync(string Id)
        {

            var product = this.dbContext.Products.Find(Id);

            this.dbContext.Products.Remove(product);

            await this.dbContext.SaveChangesAsync();
        }


    }
}
