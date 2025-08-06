using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System
{
    public class Supplier
    {
        public int Id;
        public string Name;
        public string Phone;
    }

    public class SupplierManager
    {
        private Supplier[] suppliers = new Supplier[100];
        private int count = 0;

        public void AddSupplier(Supplier s)
        {
            if (count < suppliers.Length)
            {
                suppliers[count] = s;
                count++;
            }
        }

        public Supplier GetSupplier(int id)
        {
            for (int i = 0; i < count; i++)
            {
                if (suppliers[i].Id == id)
                    return suppliers[i];
            }
            return null;
        }

        public void ListSuppliers()
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"ID: {suppliers[i].Id}, Name: {suppliers[i].Name}, Phone: {suppliers[i].Phone}");
            }
        }
    }
}
