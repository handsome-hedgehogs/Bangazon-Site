using BangazonAuth.Models;
using System.Collections.Generic;

// Class that is used in MyProductsViewModel to display how many products a customer has in stock
// and how many they have sold
// the IEnumerable SoldProductsList is populated on the viewModel with these class properties
//Authored by : Jackie Knight && Tamela Lerma
namespace BangazonAuth.Classes
{
    public class SoldProductsGroup
    {
        public int ProdQuantity { get; set; }

        public string ProdTitle { get; set; }

        public int ProdCount { get; set; }

        public int ProdId { get; set; }

        public IEnumerable<Product> SoldProductsList { get; set; }
    }
}
