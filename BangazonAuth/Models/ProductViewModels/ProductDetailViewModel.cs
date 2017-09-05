using BangazonAuth.Data;
using System.Linq;

namespace BangazonAuth.Models.ProductViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }

        public ProductDetailViewModel(ApplicationUser user, ApplicationDbContext ctx, int? id)
        {
            // Set the `Product` property of the view model
            Product = ctx.Product
                    .SingleOrDefault(prod => prod.ProductId == id);
            Product.ProductType = ctx.ProductType.SingleOrDefault(pt => pt.ProductTypeId == Product.ProductTypeId);
            Product.User = user;
        }
    }
}