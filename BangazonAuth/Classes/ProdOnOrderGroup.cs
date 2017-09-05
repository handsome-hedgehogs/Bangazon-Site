using BangazonAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonAuth.Classes
{
    public class ProdOnOrderGroup
    {
        public int ProdQuantity { get; set; }

        public string ProdTitle { get; set; }

        public int ProdCount { get; set; }

        public int ProdOrderId { get; set; }

        public IEnumerable<Product> ProdOnOrderList { get; set; }
    }
}
