using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
       
        public Product( int id ,  string name = "tea" , int quantity = 10 , decimal price= 10  )
        {
           Id = id ;
            Name = name ;
            Quantity = quantity ;
            Price = price ;
             
        } 
        public bool IsLowStock(int threshold = 5)
        {
            return Quantity <= threshold;
        }  
        public override string ToString()
        {
            return$" Name = {Name} total price =  {Quantity*Price}";
        }
    }

}
