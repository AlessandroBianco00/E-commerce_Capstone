using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.Models
{
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Editor { get; set; }
        [Required]
        public int Pages { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Language {  get; set; }
        [Required]
        public DateOnly PublicationDate { get; set; }
        [Required]
        public int QuantityAvailable { get; set; }  
        public int AuthorId { get; set; }
        public int TranslatorId { get; set; }
        [Required]
        public int DiscountId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
        [ForeignKey("TranslatorId")]
        public Translator Translator { get; set; }
        [ForeignKey("DiscountId")]
        public Discount Discount { get; set; }
        public List<Category> Categories { get; set; }  
    }
}
