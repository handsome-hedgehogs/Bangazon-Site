using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonAuth.Models
{
    public class ProductInOrder
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string Title { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }
    }
}
