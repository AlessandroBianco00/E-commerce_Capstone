using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.Entities
{
    public class Review
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        [Required]
        [Range(1, 10)] // Sarà trasformato in valutazione da 0 a 5
        public int Score { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("BookId")]
        public Book? Book { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
