using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.Models
{
    public class Translator
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TranslatorId { get; set; }
        [Required]
        public string TranslatorName { get; set; }
        [Required]
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
