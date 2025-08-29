using OnlineShop.Domain.Abstractions;
using OnlineShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Services
{
    public interface ICartService
    {
        Cart GetCart();
        void AddItem(Product product, int qty);
        void RemoveItem(int productId);
        void Clear();
        void Recalculate();
    }


    public class CartService : ICartService
    {
        private readonly Cart _cart;
        private readonly IDiscountPolicy _discount;


        public CartService(IDiscountPolicy discount)
        {
            _discount = discount;
            _cart = new Cart();
        }


        public Cart GetCart() => _cart;


        public void AddItem(Product product, int qty)
        {
            //if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
            //var existing = _cart.Items.FirstOrDefault(i => i.Product.Id == product.Id);
            //if (existing is null)
            //{
            //    _cart.Items.Add(new CartItem
            //    {
            //       // ProductId = product.Id,
            //        Name = product.Name,
            //        UnitPrice = product.Price,
            //        Quantity = qty
            //    });
            //}
            //else
            //{
            //    existing.Quantity += qty;
            //}
            //Recalculate();
        }


        public void RemoveItem(int productId)
        {
            //_cart.Items.RemoveAll(i => i.ProductId == productId);
            Recalculate();
        }


        public void Clear()
        {
            _cart.Items.Clear();
            _cart.AutoDiscount = 0m;
        }


        public void Recalculate()
        {
          //  var subtotal = _cart.Subtotal;
          //  _cart.AutoDiscount = _discount.CalculateDiscount(subtotal);
        }
    }
}
