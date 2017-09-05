using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BangazonAuth.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string StreetAddress { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<PaymentType> PaymentTypes { get; set; }
        public ICollection<Order> Orders { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public ICollection<UserLikes> UserLiked { get; set; }

        public ICollection<Recommendations> RecommendedByMe { get; set; }

        public ICollection<Recommendations> RecommendedToMe { get; set; }
    }
}
