using ApiBookStore.Models.Entities;

namespace ApiBookStore.Interfaces
{
    public interface IAuthorService
    {
        public Task<IEnumerable<Author>> GetAll();
        public Task<Author?> GetById(int id);
    }
}
