using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangazonAuth.Data;
using Microsoft.AspNetCore.Http;

namespace BangazonAuth.Models.ProductViewModels
{
    public class ProductCreateViewModel
    {
        public List<SelectListItem> ProductTypeId { get; set; }

        public Product Product { get; set; }

        public List<IFormFile> Image { get; set; } = new List<IFormFile>();

        public ProductCreateViewModel(ApplicationDbContext ctx) 
        {

            // Creating SelectListItems will be used in a @Html.DropDownList
            // control in a Razor template. See Views/Products/Create.cshtml
            // for an example.
            this.ProductTypeId = ctx.ProductType
                                    .OrderBy(l => l.Label)
                                    .AsEnumerable()
                                    .Select(li => new SelectListItem { 
                                        Text = li.Label,
                                        Value = li.ProductTypeId.ToString()
                                    }).ToList();

            this.ProductTypeId.Insert(0, new SelectListItem { 
                Text = "Choose category...",
                Value = "0"
            });
        }
        public ProductCreateViewModel() { }
    }
}