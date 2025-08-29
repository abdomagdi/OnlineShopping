using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Abstractions;
using OnlineShop.Domain.Models;
using OnlineShop.Web.Helpers;

namespace OnlineShop.Web.Controllers
{
  

    public class CartController(IProductRepository productRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await productRepository.GetAllAsync();
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            ViewBag.Cart = cart;
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var existing = cart.FirstOrDefault(c => c.Product.Id == id);
            if (existing != null)
                existing.Quantity++;
            else
                cart.Add(new CartItem { Product = product, Quantity = 1 });

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return PartialView("_CartTable", cart);
        }

        [HttpGet]
        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(c => c.Product.Id == id);

            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return PartialView("_CartTable", cart);
        }
    }
}
