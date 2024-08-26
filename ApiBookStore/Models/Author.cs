using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.Models
{
    public class Author
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Description { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
