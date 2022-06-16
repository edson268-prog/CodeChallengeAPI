using CodeChallenge.Dto.Request;
using CodeChallenge.Dto.Response;

namespace CodeChallenge.Services.Interfaces
{
    public interface IProductService
    {
        Task<BaseCollectionPageResponse<ICollection<DtoResponseProduct>>> FilterAsync(string? filter, int page, int rows);
        Task<BaseResponseGeneric<DtoResponseProduct>> GetByIdAsync(int id);
        Task<BaseResponseGeneric<int>> CreateAsync(DtoProduct request);        
        Task<BaseResponse> UpdateAsync(int id, DtoProduct request);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponse> PatchAsync(int id);
    }
}