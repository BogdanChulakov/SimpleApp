using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApp.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles= "Administrator")]
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
