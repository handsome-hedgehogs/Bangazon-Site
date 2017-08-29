using BangazonAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonAuth.Models.ProductViewModels
{
    public class MyProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
