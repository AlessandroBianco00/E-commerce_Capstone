using ApiBookStore.DTO;
using ApiBookStore.Entities;

namespace ApiBookStore.Interfaces
{
    public interface IAuthorService
    {
        public Task<IEnumerable<Author>> GetAll();
        public Task<AuthorDetailDto?> GetById(int id);
    }
}
