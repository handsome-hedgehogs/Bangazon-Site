using System.Collections.Generic;
using BangazonAuth.Models;
using BangazonAuth.Data;

namespace BangazonAuth.Models.ProductViewModels
{
  public class ProductListViewModel
  {
    public IEnumerable<Product> Products { get; set; }
  }

}