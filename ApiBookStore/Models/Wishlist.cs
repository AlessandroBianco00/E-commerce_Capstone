using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PizzeriaWebApp.Models.Entities;

namespace ApiBookStore.Models
{
    public class Wishlist
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WishlistId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public List<Book> Books { get; set; }
    }
}
