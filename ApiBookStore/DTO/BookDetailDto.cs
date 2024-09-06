using ApiBookStore.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.DTO
{
    public class BookDetailDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Editor { get; set; }
        public int Pages { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; }
        public DateOnly PublicationDate { get; set; }
        public int QuantityAvailable { get; set; }
        public int AuthorId { get; set; }
        public int TranslatorId { get; set; }
        public int DiscountId { get; set; }
        public AuthorSearchDto Author { get; set; }
        public TranslatorSearchDto Translator { get; set; }
        public Discount Discount { get; set; }
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
    }
}
