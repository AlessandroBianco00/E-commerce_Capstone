using ApiBookStore.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.DTO
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}
