using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;

namespace ApiBookStore.Models.Entities
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        [Phone]
        [RegularExpression(@"^\+?\d{1,14}$", ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Password { get; set; }
        public DateTime? DeletedAt { get; set; }

        public List<ShippingAddress> ShippingAddresses { get; set; } = new List<ShippingAddress>();

        public List<Role> Roles { get; set; } = new List<Role>();

        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<Order> Orders { get; set; } = new List<Order>();

        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
