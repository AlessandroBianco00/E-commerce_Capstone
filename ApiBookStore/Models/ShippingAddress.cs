using PizzeriaWebApp.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.Models
{
    public class ShippingAddress
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShippingAddressId { get; set; }

        [Required]
        [StringLength(100)]
        public string StreetAddress { get; set; }  

        [Required]
        [StringLength(50)]
        public string City { get; set; } 

        [Required]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid ZIP Code format")]
        public int ZipCode { get; set; }  

        [Required]
        [StringLength(40)]
        public string Country { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
