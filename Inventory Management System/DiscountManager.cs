using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System
{
    public class DiscountManager
    {
        private int[] productIds = new int[100];
        private decimal[] discounts = new decimal[100];
        private int count = 0;

        public void SetDiscount(int productId, decimal discountPercentage)
        {
            for (int i = 0; i < count; i++)
            {
                if (productIds[i] == productId)
                {
                    discounts[i] = discountPercentage;
                    return;
                }
            }

            if (count < productIds.Length)
            {
                productIds[count] = productId;
                discounts[count] = discountPercentage;
                count++;
            }
        }
        // we must set the discount then get the discount with same ID 
        public decimal GetDiscountedPrice(Product p)
        {
            for (int i = 0; i < count; i++)
            {
                if (productIds[i] == p.Id)
                {
                    decimal discount = discounts[i];
                    return p.Price * (1 - discount);
                }
            }
            return p.Price;
        }
    }
}
