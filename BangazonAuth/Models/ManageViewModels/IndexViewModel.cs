using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangazonAuth.Data;

namespace BangazonAuth.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        //public string PhoneNumber { get; set; }

        //public string FirstName { get; set; }

        //public string LastName { get; set; }

        //public string Address { get; set; }

        //public string Email { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

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
