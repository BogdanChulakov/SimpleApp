using AutoMapper;
using SimpleApp.Data.Models;
using SimpleApp.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApp
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, OutputProductViewModel>();
            CreateMap<Product, AdminProductViewModel>();

        }
    }
}
