using AutoMapper;
using CodeChallenge.DataAccess.Repositories;
using CodeChallenge.Dto.Request;
using CodeChallenge.Dto.Response;
using CodeChallenge.Services.Interfaces;

namespace CodeChallenge.Services.Implementations
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository _repository;
        private readonly IMapper _mapper;

        public ProductTypeService(IProductTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponseGeneric<ICollection<DtoResponseProductType>>> FilterAsync(string? filter)
        {
            var response = new BaseResponseGeneric<ICollection<DtoResponseProductType>>();
            try
            {
                //var collection = await _repository.FilterAsync(filter);
                var collection = await _repository.FilterAsync(x => x.Name.Contains(filter)); //Predicate implemented

                response.ResponseResult = collection;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ListErrors.Add(ex.Message);
                response.Success = false;
            }

            return response;
        }

        public async Task<BaseResponseGeneric<DtoResponseProductType>> GetByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<DtoResponseProductType>();
            var entity = await _repository.GetByIdAsync(id);

            response.ResponseResult = _mapper.Map<DtoResponseProductType>(entity);
            response.Success = true;

            return response;
        }

        public async Task<BaseResponseGeneric<int>> CreateAsync(DtoProductType request)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                response.ResponseResult = await _repository.CreateAsync(request);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ListErrors.Add(ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, DtoProductType request)
        {
            var response = new BaseResponse();

            try
            {
                //var entity = await _repository.GetByIdAsync(id);
                await _repository.UpdateAsync(id, request);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ListErrors.Add(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();

            try
            {
                await _repository.DeleteAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ListErrors.Add(ex.Message);
            }

            return response;
        }
    }
}
