using ApiBookStore.DTO;
using ApiBookStore.Entities;
using ApiBookStore.Models;

namespace ApiBookStore.Interfaces
{
    public interface IBookService
    {
        public Task<Book> Create(BookModel boookModel);
        public Task<IEnumerable<Book>> GetAll();
        public Task<Book?> GetById(int id);
        public Task<BookDetailDto?> GetBookDetail(int id);
        public Task<IEnumerable<BookSearchDto>> GetBooksByCategoryId(int categoryId);
    }
}
