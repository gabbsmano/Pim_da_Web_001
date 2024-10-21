using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Pim_da_Web_001.Models;

namespace Pim_da_Web_001.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Estoque()
        {
            return View();
        }

        public IActionResult Fornecedor()
        {
            return View();
        }

        public IActionResult Colaboradores()
        {
            return View();
        }

        public IActionResult Cliente()
        {
            return View();
        }

        public IActionResult Login() 
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
