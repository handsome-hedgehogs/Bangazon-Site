using BangazonAuth.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonAuth.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

        public int? PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
