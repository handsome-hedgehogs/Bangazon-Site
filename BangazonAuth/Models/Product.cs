using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BangazonAuth.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date Added")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(55)]
        public string Title { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public bool LocalDelivery { get; set; }

        [Required]
        public string PhotoURL { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int ProductTypeId { get; set; }

        [Display(Name = "Category")]
        public ProductType ProductType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price > 10000)
            {
                yield return new ValidationResult("Please contact our customer service department to sell something of this value.");
            }
        }

        public ICollection<Rating> Ratings { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

        public ICollection<Recommendations> Recommended { get; set; }

        public ICollection<UserLikes> UserLiked { get; set; }
    }
}