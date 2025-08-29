namespace OnlineShop.Domain.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; } = new();
       // public decimal Subtotal => Items.Sum(i => i.LineSubtotal);
        public decimal AutoDiscount { get; set; } // computed via policies
      //  public decimal Total => Subtotal - AutoDiscount;
    }
}
