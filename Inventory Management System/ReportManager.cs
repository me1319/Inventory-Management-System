using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System
{
    public class ReportManager
    {
        private SalesManager salesManager;
        private Inventory inventory;

        public ReportManager(SalesManager sales, Inventory inv)
        {
            salesManager = sales;
            inventory = inv;
        }

        public Product GetBestSeller()
        {
            Product best = null;
            int maxSold = -1;

            Product[] products = inventory.GetAllProducts();

            for (int i = 0; i < products.Length; i++)
            {
                int sold = salesManager.GetTotalUnitsSold(products[i].Id);
                if (sold > maxSold)
                {
                    maxSold = sold;
                    best = products[i];
                }
            }

            return best;
        }

        public void PrintLowStock()
        {
            Product[] lowStock = inventory.GetLowStockProducts();
            for (int i = 0; i < lowStock.Length; i++)
            {
                Console.WriteLine("product: " + lowStock[i].Name + " quantity: " + lowStock[i].Quantity);
            }
        }
    }
}
