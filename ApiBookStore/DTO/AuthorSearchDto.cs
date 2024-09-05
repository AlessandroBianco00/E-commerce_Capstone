using ApiBookStore.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.DTO
{
    public class AuthorSearchDto
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
