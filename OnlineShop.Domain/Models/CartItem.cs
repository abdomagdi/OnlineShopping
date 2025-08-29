namespace OnlineShop.Domain.Models
{
    public class CartItem
    {
        public Product Product { get; set; }   // المنتج نفسه
        public int Quantity { get; set; }      // الكمية

        public decimal TotalPrice
        {
            get
            {
                // السعر بعد الخصم
                var discountAmount = Product.Price * (decimal)(Product.DiscountPercent / 100);
                var priceAfterDiscount = Product.Price - discountAmount;
                return priceAfterDiscount * Quantity;
            }
        }
    }
}
