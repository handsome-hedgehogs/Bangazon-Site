﻿using BangazonAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonAuth.Models.ProductViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public Order Order { get; set; }
    }
}
