using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System
{
    public class SalesManager
    {
        private Transaction[] transactions = new Transaction[100];
        private int count = 0;

        public void RecordSale(Product product, int quantity)
        {
            if (product != null && product.Quantity >= quantity)
            {
                product.Quantity -= quantity;

                Transaction t = new Transaction();
                t.TransactionId = count + 1;
                t.ProductId = product.Id;
                t.QuantitySold = quantity;
                t.Date = DateTime.Now;
                t.TotalPrice = product.Price * quantity;

                transactions[count] = t;
                count++;
            }
            else
            {
                Console.WriteLine("out of stock .");
            }
        }

        public decimal GetTotalSales()
        {
            decimal total = 0;
            for (int i = 0; i < count; i++)
            {
                total += transactions[i].TotalPrice;
            }
            return total;
        }

        public int GetTotalUnitsSold(int productId)
        {
            int total = 0;
            for (int i = 0; i < count; i++)
            {
                if (transactions[i].ProductId == productId)
                {
                    total += transactions[i].QuantitySold;
                }
            }
            return total;
        }

        public Transaction[] GetAllTransactions()
        {
            Transaction[] result = new Transaction[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = transactions[i];
            }
            return result;
        }
    }
}
