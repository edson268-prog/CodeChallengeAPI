using CodeChallenge.Dto.Request;
using CodeChallenge.Dto.Response;
using CodeChallenge.Entities;

namespace CodeChallenge.DataAccess.Repositories
{
    public interface IProductRepository
    {
        Task<(ICollection<DtoResponseProduct> collection, int total)> FilterAsync(string? filter, int page, int rows);
        Task<Product?> GetByIdAsync(int id);
        Task<int> CreateAsync(DtoProduct request);
        Task UpdateAsync(int id, DtoProduct request);
        Task DeleteAsync(int id);
        Task PatchAsync(int id);
    }
}