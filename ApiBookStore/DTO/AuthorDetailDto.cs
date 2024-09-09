using ApiBookStore.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.DTO
{
    public class AuthorDetailDto
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public List<BookSearchDto> Books { get; set; } = new List<BookSearchDto>();
    }
}
