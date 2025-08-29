using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.Domain.Abstractions;
using OnlineShop.Domain.Models;
using OnlineShop.Web.Models;
using System.Diagnostics;

namespace OnlineShop.Web.Controllers
{
    public class HomeController(ILogger<HomeController> logger,IProductRepository productRepository) : Controller
    {
        //private readonly ILogger<HomeController> _logger = logger;
        [Route("/")]
        [Route("/Home")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            logger.LogInformation("Fetching all products for the home page.");
            IEnumerable<Product> products =await productRepository.GetAllAsync();
            return View(products);
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
