using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DIDemo.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DIDemo.Controllers
{
    public class HomeController : Controller
    {
        private ITest _test;
        private ILogger<HomeController> _logger;

        public HomeController(ITest test, ILogger<HomeController> logger)
        {
            this._test = test;
            this._logger = logger;
        }

        public IActionResult Index()
        {
            //通过构造函数获取
            var res1 = this._test;
            ViewBag.TestFromConstructor = res1;

            //通过HttpContext获取
            var res2 = HttpContext.RequestServices.GetService<ITest>();
            ViewBag.TestFromContext = res2;

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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
