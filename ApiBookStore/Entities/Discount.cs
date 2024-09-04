using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.Entities
{
    public class Discount
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiscountId { get; set; }

        [Required]
        [StringLength(100)]
        public string DiscountName { get; set; }

        [Required]
        [Range(1, 100)]
        public int DiscountPercentage { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateOnly EndingDate { get; set; }
    }
}
