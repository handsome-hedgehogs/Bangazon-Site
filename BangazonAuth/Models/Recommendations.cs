using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BangazonAuth.Models;

namespace BangazonAuth.Models
{
    public class Recommendations
    {
        [Key]
        public int RecommendationId { get; set; }

        [Required]
        public virtual ApplicationUser Recommender { get; set; }

        [Required]
        public virtual ApplicationUser Recommendee { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public bool Done { get; set; }
    }
}
