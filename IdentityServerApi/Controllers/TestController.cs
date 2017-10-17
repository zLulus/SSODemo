using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerApi.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Redis()
        {
            return View();
        }
    }
}