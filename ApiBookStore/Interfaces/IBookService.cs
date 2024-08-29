using ApiBookStore.Models;
using ApiBookStore.Models.Entities;

namespace ApiBookStore.Interfaces
{
    public interface IBookService
    {
        public Task<Book> Create(BookModel boookModel);
        public Task<IEnumerable<Book>> GetAll();
        public Task<Book?> GetById(int id);
    }
}
