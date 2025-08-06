using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System
{
    public class Inventory
    {
        private Product[] products = new Product[100];
        private int count = 0;
        public void firstScreen()
        {
            Console.WriteLine("welcome to IMS");
            selectRole();
        }

        public void selectRole()
        {
            Console.WriteLine("Use these letters:");
            Console.WriteLine("u  =============== user");
            Console.WriteLine("s  =============== supplier");
            Console.WriteLine("m  =============== manager");
            string n = Console.ReadLine()?.ToLower();
            roles(n);


        }

        public void roles(string roles)
        {

            switch (roles)
            {
                case "u":
                    Console.WriteLine("User selected");
                    return;
                case "s":
                    Console.WriteLine("Supplier selected");

                    return;

                case "m":
                    Console.WriteLine("Manager selected");

                    return;

                default:
                    Console.WriteLine("Invalid input. Try again!");
                    break;
            }
        }
        public void AddProduct(Product p)
        {
            if (count < products.Length)
            {
                products[count] = p;
                count++;
            }
        } 

        public void RemoveProduct(int id)
        {
            for (int i = 0; i < count; i++)
            {
                if (products[i].Id == id)
                {
                    for (int j = i; j < count - 1; j++)
                    {
                        products[j] = products[j + 1];
                    }
                    products[count - 1] = null;
                    count--;
                    break;
                }
            }
        }

        public Product GetProductById(int id)
        {
            for (int i = 0; i < count; i++)
            {
                if (products[i].Id == id)
                    return products[i];
            }
            return null;
        }

        public void UpdateProduct(Product updatedProduct)
        {
            for (int i = 0; i < count; i++)
            {
                if (products[i].Id == updatedProduct.Id)
                {
                    products[i].Name = updatedProduct.Name;
                    products[i].Quantity = updatedProduct.Quantity;
                    products[i].Price = updatedProduct.Price;
                    break;
                }
            }
        }

        public Product[] GetLowStockProducts(int threshold = 5)
        {
            Product[] result = new Product[100];
            int resultCount = 0;

            for (int i = 0; i < count; i++)
            {
                if (products[i].IsLowStock(threshold))
                {
                    result[resultCount] = products[i];
                    resultCount++;
                }
            }

            // Resize array
            Product[] finalResult = new Product[resultCount];
            for (int i = 0; i < resultCount; i++)
            {
                finalResult[i] = result[i];
            }

            return finalResult;
        }

        public Product[] GetAllProducts()
        {
            Product[] result = new Product[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = products[i];
            }
            return result;
        }
    }
}
