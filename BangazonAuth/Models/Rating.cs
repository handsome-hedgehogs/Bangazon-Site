using BangazonAuth.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonAuth.Models
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [Range(0, 5)]
        public int ProductRating { get; set; }
    }
}
