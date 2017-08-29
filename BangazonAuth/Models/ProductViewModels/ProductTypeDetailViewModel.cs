using System.Collections.Generic;
using BangazonAuth.Models;
using BangazonAuth.Data;

namespace BangazonAuth.Models.ProductViewModels
{
  public class ProductTypeDetailViewModel
  {
    public ProductType ProductType { get; set; }
    public List<Product> Products { get; set; }
  }
}