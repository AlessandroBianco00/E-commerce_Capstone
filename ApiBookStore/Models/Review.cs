using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PizzeriaWebApp.Models.Entities;

namespace ApiBookStore.Models
{
    public class Review
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }
        [Required]
        public int Score { get; set; }
        [Required]
        public string Description { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
