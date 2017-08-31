using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BangazonAuth.Models
{
  public class PaymentType
  {
    [Key]
    public int PaymentTypeId { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime DateCreated { get;set; }

    [Required]
    [StringLength(12)]
    [Display(Name ="Payment Type")]
    public string Description { get; set; }

    [Required]
    [StringLength(20)]
    [Display(Name ="Account Number")]
    public string AccountNumber { get; set; }

    [Required]
    public virtual ApplicationUser User { get; set; }
  }
}
