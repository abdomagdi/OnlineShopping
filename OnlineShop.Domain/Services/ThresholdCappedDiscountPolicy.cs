using OnlineShop.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Services
{
    public class ThresholdCappedDiscountPolicy : IDiscountPolicy
    {
        private readonly decimal _threshold;
        private readonly decimal _percent; // e.g. 0.10m = 10%
        private readonly decimal _maxDiscount;


        public ThresholdCappedDiscountPolicy(decimal threshold, decimal percent, decimal maxDiscount)
        {
            _threshold = threshold;
            _percent = percent;
            _maxDiscount = maxDiscount;
        }


        public decimal CalculateDiscount(decimal subtotal)
        {
            if (subtotal < _threshold) return 0m;
            var raw = subtotal * _percent;
            return Math.Min(raw, _maxDiscount);
        }
    }
}
