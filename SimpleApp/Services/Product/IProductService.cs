using SimpleApp.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApp.Services.Product
{
    public interface IProductService
    {
        List<OutputProductViewModel> All<T>();

        Task AddAsync(string name, string description, double price);
    }
}
