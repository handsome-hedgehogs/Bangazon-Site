using System.Collections.Generic;


namespace BangazonAuth.Models.ProductViewModels
{
  public class ProductTypeDetailViewModel
  {
    public ProductType ProductType { get; set; }
    public List<Product> Products { get; set; }
  }
}