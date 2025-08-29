using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
    //    [Required]
        public string Name { get; set; } = string.Empty;
      //  [Required]
        public string Description { get; set; } = string.Empty;
    //    [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
    //    [Range(1,100)]
        public decimal? DiscountPercent { get; set; }       
    }
}
