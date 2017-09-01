using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonAuth.Models
{
    public class SoldProductsGroup
    {
        public int ProdQuantity { get; set; }
        public int ProdId { get; set; }
        public string ProdTitle { get; set; }
        public int ProdCount { get; set; }
        public int ProdUnSold { get; set; }
        public IEnumerable<Product> SoldProductsList { get; set; }
    }
}
