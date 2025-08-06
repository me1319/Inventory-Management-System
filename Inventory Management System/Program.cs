using System;

namespace Inventory_Management_System
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Inventory inventory = new Inventory();
            SalesManager salesManager = new SalesManager();
            DiscountManager discountManager = new DiscountManager();
            SupplierManager supplierManager = new SupplierManager();
            ReportManager reportManager = new ReportManager(salesManager, inventory);

            
            inventory.firstScreen();

            // Sample data setup
            InitializeSampleData(inventory, supplierManager, discountManager);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n===== MAIN MENU =====");
                Console.WriteLine("1. Product Management");
                Console.WriteLine("2. Sales Management");
                Console.WriteLine("3. Discount Management");
                Console.WriteLine("4. Supplier Management");
                Console.WriteLine("5. Reports");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ProductManagementMenu(inventory);
                        break;
                    case "2":
                        SalesManagementMenu(inventory, salesManager, discountManager);
                        break;
                    case "3":
                        DiscountManagementMenu(inventory, discountManager);
                        break;
                    case "4":
                        SupplierManagementMenu(supplierManager);
                        break;
                    case "5":
                        ReportsMenu(reportManager, inventory);
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }

            Console.WriteLine("Thank you for using the Inventory Management System!");
        }

        static void InitializeSampleData(Inventory inventory, SupplierManager supplierManager, DiscountManager discountManager)
        {
            // Add sample products with IDs
            inventory.AddProduct(new Product(1, "Laptop", 15, 999.99m));
            inventory.AddProduct(new Product(2, "Smartphone", 25, 699.99m));
            inventory.AddProduct(new Product(3, "Headphones", 4, 149.99m)); // Low stock item
            inventory.AddProduct(new Product(4, "Mouse", 50, 24.99m));
            inventory.AddProduct(new Product(5, "Keyboard", 2, 89.99m)); // Low stock item

            // Add sample suppliers
            supplierManager.AddSupplier(new Supplier { Id = 1, Name = "Tech Supplies Inc.", Phone = "555-1234" });
            supplierManager.AddSupplier(new Supplier { Id = 2, Name = "Gadget World", Phone = "555-5678" });

            // Add sample discounts
            discountManager.SetDiscount(1, 0.10m); // 10% discount on Laptops
            discountManager.SetDiscount(3, 0.15m); // 15% discount on Headphones
        }

        static void ProductManagementMenu(Inventory inventory)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n===== PRODUCT MANAGEMENT =====");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Remove Product");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. View All Products");
                Console.WriteLine("5. View Low Stock Products");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter product ID: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Enter product name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter quantity: ");
                        int quantity = int.Parse(Console.ReadLine());
                        Console.Write("Enter price: ");
                        decimal price = decimal.Parse(Console.ReadLine());

                        Product newProduct = new Product(id, name, quantity, price);
                        inventory.AddProduct(newProduct);
                        Console.WriteLine("Product added successfully!");
                        break;
                    case "2":
                        Console.Write("Enter product ID to remove: ");
                        int removeId = int.Parse(Console.ReadLine());
                        inventory.RemoveProduct(removeId);
                        Console.WriteLine("Product removed successfully!");
                        break;
                    case "3":
                        Console.Write("Enter product ID to update: ");
                        int updateId = int.Parse(Console.ReadLine());
                        Product existing = inventory.GetProductById(updateId);
                        if (existing != null)
                        {
                            Console.Write($"Enter new name (current: {existing.Name}): ");
                            string newName = Console.ReadLine();
                            Console.Write($"Enter new quantity (current: {existing.Quantity}): ");
                            int newQuantity = int.Parse(Console.ReadLine());
                            Console.Write($"Enter new price (current: {existing.Price}): ");
                            decimal newPrice = decimal.Parse(Console.ReadLine());

                            Product updated = new Product(existing.Id, newName, newQuantity, newPrice);
                            inventory.UpdateProduct(updated);
                            Console.WriteLine("Product updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Product not found!");
                        }
                        break;
                    case "4":
                        Console.WriteLine("\n===== ALL PRODUCTS =====");
                        Product[] allProducts = inventory.GetAllProducts();
                        foreach (var product in allProducts)
                        {
                            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Quantity: {product.Quantity}, Price: {product.Price:C}");
                        }
                        break;
                    case "5":
                        Console.WriteLine("\n===== LOW STOCK PRODUCTS =====");
                        Product[] lowStock = inventory.GetLowStockProducts();
                        foreach (var product in lowStock)
                        {
                            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Quantity: {product.Quantity}");
                        }
                        break;
                    case "6":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void SalesManagementMenu(Inventory inventory, SalesManager salesManager, DiscountManager discountManager)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n===== SALES MANAGEMENT =====");
                Console.WriteLine("1. Record Sale");
                Console.WriteLine("2. View Total Sales");
                Console.WriteLine("3. View All Transactions");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter product ID: ");
                        int productId = int.Parse(Console.ReadLine());
                        Product product = inventory.GetProductById(productId);
                        if (product != null)
                        {
                            Console.Write("Enter quantity to sell: ");
                            int quantity = int.Parse(Console.ReadLine());

                            // Get discounted price if available
                            decimal originalPrice = product.Price * quantity;
                            decimal discountedPrice = discountManager.GetDiscountedPrice(product) * quantity;

                            if (originalPrice != discountedPrice)
                            {
                                Console.WriteLine($"Original price: {originalPrice:C}");
                                Console.WriteLine($"Discounted price: {discountedPrice:C} (Discount applied!)");
                            }

                            salesManager.RecordSale(product, quantity);
                            Console.WriteLine("Sale recorded successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Product not found!");
                        }
                        break;
                    case "2":
                        decimal totalSales = salesManager.GetTotalSales();
                        Console.WriteLine($"Total Sales: {totalSales:C}");
                        break;
                    case "3":
                        Console.WriteLine("\n===== ALL TRANSACTIONS =====");
                        var transactions = salesManager.GetAllTransactions();
                        foreach (var transaction in transactions)
                        {
                            Product p = inventory.GetProductById(transaction.ProductId);
                            string productName = p != null ? p.Name : "Unknown Product";
                            Console.WriteLine($"ID: {transaction.TransactionId}, Product: {productName}, Quantity: {transaction.QuantitySold}, Date: {transaction.Date}, Total: {transaction.TotalPrice:C}");
                        }
                        break;
                    case "4":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void DiscountManagementMenu(Inventory inventory, DiscountManager discountManager)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n===== DISCOUNT MANAGEMENT =====");
                Console.WriteLine("1. Set Discount for Product");
                Console.WriteLine("2. View Products with Discounts");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter product ID: ");
                        int productId = int.Parse(Console.ReadLine());
                        Product product = inventory.GetProductById(productId);
                        if (product != null)
                        {
                            Console.Write("Enter discount percentage (0.00 to 1.00): ");
                            decimal discount = decimal.Parse(Console.ReadLine());
                            discountManager.SetDiscount(productId, discount);
                            Console.WriteLine("Discount set successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Product not found!");
                        }
                        break;
                    case "2":
                        Console.WriteLine("\n===== PRODUCTS WITH DISCOUNTS =====");
                        Product[] allProducts = inventory.GetAllProducts();
                        foreach (var prod in allProducts)
                        {
                            decimal originalPrice = prod.Price;
                            decimal discountedPrice = discountManager.GetDiscountedPrice(prod);
                            if (originalPrice != discountedPrice)
                            {
                                decimal discountPercentage = 1 - (discountedPrice / originalPrice);
                                Console.WriteLine($"ID: {prod.Id}, Name: {prod.Name}, Original Price: {originalPrice:C}, Discounted Price: {discountedPrice:C} ({discountPercentage:P} off)");
                            }
                        }
                        break;
                    case "3":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void SupplierManagementMenu(SupplierManager supplierManager)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n===== SUPPLIER MANAGEMENT =====");
                Console.WriteLine("1. Add Supplier");
                Console.WriteLine("2. View Supplier");
                Console.WriteLine("3. List All Suppliers");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Supplier newSupplier = new Supplier();
                        Console.Write("Enter supplier ID: ");
                        newSupplier.Id = int.Parse(Console.ReadLine());
                        Console.Write("Enter supplier name: ");
                        newSupplier.Name = Console.ReadLine();
                        Console.Write("Enter supplier phone: ");
                        newSupplier.Phone = Console.ReadLine();
                        supplierManager.AddSupplier(newSupplier);
                        Console.WriteLine("Supplier added successfully!");
                        break;
                    case "2":
                        Console.Write("Enter supplier ID to view: ");
                        int supplierId = int.Parse(Console.ReadLine());
                        Supplier supplier = supplierManager.GetSupplier(supplierId);
                        if (supplier != null)
                        {
                            Console.WriteLine($"ID: {supplier.Id}, Name: {supplier.Name}, Phone: {supplier.Phone}");
                        }
                        else
                        {
                            Console.WriteLine("Supplier not found!");
                        }
                        break;
                    case "3":
                        Console.WriteLine("\n===== ALL SUPPLIERS =====");
                        supplierManager.ListSuppliers();
                        break;
                    case "4":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void ReportsMenu(ReportManager reportManager, Inventory inventory)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n===== REPORTS =====");
                Console.WriteLine("1. View Best Selling Product");
                Console.WriteLine("2. View Low Stock Products");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Product bestSeller = reportManager.GetBestSeller();
                        if (bestSeller != null)
                        {
                            Console.WriteLine($"Best Selling Product: {bestSeller.Name}");
                        }
                        else
                        {
                            Console.WriteLine("No sales data available.");
                        }
                        break;
                    case "2":
                        Console.WriteLine("\n===== LOW STOCK PRODUCTS =====");
                        reportManager.PrintLowStock();
                        break;
                    case "3":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}