using SimpleApp.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApp.Services.Product
{
    public interface IProductService
    {
        List<Data.Models.Product> All();

        Task AddAsync(string name, string description, double price);

        Task DeleteAsync(string Id);
    }
}
