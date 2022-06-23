using CodeChallenge.Dto.Request;
using CodeChallenge.Dto.Response;
using CodeChallenge.Entities;
using System.Linq.Expressions;

namespace CodeChallenge.DataAccess.Repositories
{
    public interface IProductTypeRepository
    {
        //Task<ICollection<DtoResponseProductType>> FilterAsync(string? filter);
        Task<ICollection<DtoResponseProductType>> FilterAsync(Expression<Func<ProductType, bool>> expression);
        Task<ProductType?> GetByIdAsync(int id);
        Task<int> CreateAsync(DtoProductType request);
        Task UpdateAsync(int id, DtoProductType request);
        Task DeleteAsync(int id);
    }
}