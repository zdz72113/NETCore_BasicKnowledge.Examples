using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestConfigration.Models;

namespace TestConfigration.Controllers
{
    public class HomeController : Controller
    {
        public TestSubSectionConfig _subSectionConfig;
        public ILogger<HomeController> _logger; 

        public HomeController(IOptions<TestSubSectionConfig> option, ILogger<HomeController> logger)
        {
            _subSectionConfig = option.Value;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation($"SubOption1: {_subSectionConfig.SubOption1}");
            _logger.LogInformation($"SubOption2: {_subSectionConfig.SubOption2}");
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
