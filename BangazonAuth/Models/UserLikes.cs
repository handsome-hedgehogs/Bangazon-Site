using BangazonAuth.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonAuth.Models
{
    public class UserLikes
    {
        [Key]
        public int UserLikeId { get; set; }

        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]  // maybe not  required if someone wants to unlike, but not dislike or vice-versa
        public bool Like { get; set; }
    }
}
