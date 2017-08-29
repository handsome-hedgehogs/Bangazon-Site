using System.Collections.Generic;
using BangazonAuth.Models;
using BangazonAuth.Data;

namespace BangazonAuth.Models.ProductViewModels
{
    public class ProductTypesViewModel
    {
        public IEnumerable<ProductType> ProductTypes { get; set; }
    }
}