using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.Entities
{
    public class Translator
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TranslatorId { get; set; }

        [Required]
        [StringLength(100)]
        public string TranslatorName { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
