using CodeChallenge.Dto.Request;
using CodeChallenge.Dto.Response;

namespace CodeChallenge.Services.Interfaces
{
    public interface IProductTypeService
    {
        Task<BaseResponseGeneric<int>> CreateAsync(DtoProductType request);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponseGeneric<ICollection<DtoResponseProductType>>> FilterAsync(string? filter);
        Task<BaseResponseGeneric<DtoResponseProductType>> GetByIdAsync(int id);
        Task<BaseResponse> UpdateAsync(int id, DtoProductType request);
    }
}