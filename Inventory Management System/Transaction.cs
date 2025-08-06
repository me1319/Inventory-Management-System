using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System
{
    public class Transaction
    {
        public int TransactionId;
        public int ProductId;
        public int QuantitySold;
        public DateTime Date;
        public decimal TotalPrice;
    }
}
