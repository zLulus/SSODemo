using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebsiteB.Models;
using Microsoft.AspNetCore.Authorization;
using WebsiteB.Helpers;
using Microsoft.AspNetCore.Http;

namespace WebsiteB.Controllers
{
    public class HomeController : FonourControllerBase
    {
        public IActionResult Index()
        {
            ViewBag.Account= HttpContext.Session.GetString("Account");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
