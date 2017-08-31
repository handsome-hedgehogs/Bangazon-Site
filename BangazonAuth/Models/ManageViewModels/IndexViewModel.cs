using System.Collections.Generic;

// View Model that Handles User Profile Information
// Instance of ApplicationUser is created which inherit from Identity
// ICollection for PaymentType, Order, and Produt so user can have access to payments methods
// ability to add  payment
// and ability to view Order that are completed
// ViewModel is used in MaganerController for Index.cshtml and EditProfile.cshtml
// Authored by : Tamela Lerma
namespace BangazonAuth.Models.ManageViewModels
{
    public class IndexViewModel
    {

        public ApplicationUser AppUser { get; set; }

        public ICollection<PaymentType> PaymentTypes { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Product> Products { get; set; }


        public IndexViewModel()
        {
            AppUser = new ApplicationUser();
             

        }



    }
        
}
