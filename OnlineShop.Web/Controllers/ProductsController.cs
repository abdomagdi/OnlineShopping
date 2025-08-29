using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Abstractions;
using OnlineShop.Domain.Models;

namespace OnlineShop.Web.Controllers
{
    public class ProductsController (ILogger<HomeController> logger, IProductRepository productRepository) : Controller
    {
        //[Route("/")]
        //  [Route("Products/")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            logger.LogInformation("Getting All Products");
            IEnumerable<Product> prodoucts = await productRepository.GetAllAsync();
            return View(prodoucts);
        }

        public async Task<IActionResult> LoadTable()
        {
            var products = await productRepository.GetAllAsync();
            return PartialView("_ProductsTable", products);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                    await productRepository.AddAsync(product);
                else
                    await productRepository.UpdateAsync(product);

                var products = await productRepository.GetAllAsync();
                return PartialView("_ProductsTable", products);
            }
            return BadRequest("Invalid data");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var prod = await productRepository.GetByIdAsync(id);
            if (prod != null)
            {
                await productRepository.DeleteAsync(id);
            }

            var products = await productRepository.GetAllAsync();
            return PartialView("_ProductsTable", products);
        }


        //[HttpPost]
        //public async Task<IActionResult> Save(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (product.Id == 0)
        //        {
        //            // Add
        //            await productRepository.AddAsync(product);
        //            TempData["Message"] = "Product added successfully!";
        //        }
        //        else
        //        {
        //            // Edit
        //            await productRepository.UpdateAsync(product);
        //            TempData["Message"] = "Product updated successfully!";
        //        }


        //        return Json(new { success = true });
        //    }

        //    return Json(new { success = false, message = "Invalid data" });
        //}

        ////[HttpPost]
        ////public async Task<IActionResult> Create(Product product)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        await productRepository.AddAsync(product);

        ////        return Json(new
        ////        {
        ////            success = true,
        ////            data = new
        ////            {
        ////                product.Id,
        ////                product.Name,
        ////                product.Description,
        ////                product.Price,
        ////                product.DiscountPercent
        ////            }
        ////        });
        ////    }

        ////    return Json(new { success = false, message = "Validation failed" });
        ////}

        //[HttpPost]
        //public async Task<IActionResult> Edit(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await productRepository.UpdateAsync(product);              
        //        return Json(new { success = true, message = "Product updated successfully" });
        //    }
        //    return Json(new { success = false, message = "Invalid data" });
        //}

        //[HttpPost]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var product = await productRepository.GetByIdAsync(id);
        //    if (product == null)
        //        return Json(new { success = false, message = "Product not found" });

        //    await productRepository.DeleteAsync(id);

        //    return Json(new { success = true, message = "Product deleted successfully" });
        //}
    }
}
