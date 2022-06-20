using AutoMapper;
using AutoMapper.QueryableExtensions;
using CodeChallenge.DataAccess.Repositories;
using CodeChallenge.Dto;
using CodeChallenge.Dto.Request;
using CodeChallenge.Dto.Response;
using CodeChallenge.Entities;
using CodeChallenge.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CodeChallenge.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseCollectionPageResponse<ICollection<DtoResponseProduct>>> FilterAsync(string? filter, int page, int rows)
        {
            var response = new BaseCollectionPageResponse<ICollection<DtoResponseProduct>>();
            try
            {
                var tuple = await _repository.FilterAsync(filter, page, rows); //tuple para almacenar las dos variables de retorno

                response.ResponseResult = tuple.collection;
                response.TotalPages = Utils.GetTotalPages(tuple.total, rows);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ListErrors.Add(ex.Message);
                response.Success = false;
            }

            return response;

        }
        public async Task<BaseResponseGeneric<DtoResponseProduct>> GetByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<DtoResponseProduct>();
            var entity = await _repository.GetByIdAsync(id);

            response.ResponseResult = _mapper.Map<DtoResponseProduct>(entity);
            response.Success = true;

            return response;
        }

        public async Task<BaseResponseGeneric<int>> CreateAsync(DtoProduct request)
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

        public async Task<BaseResponse> UpdateAsync(int id, DtoProduct request)
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

        //TODO: There is another way to have Fixed responses based on an interface, with custom codes instead of creating an object on each service response
        public async Task<IGenericBaseResponse<bool>> DeleteAsync(int id)
        {
            //var response = new BaseResponse();

            try
            {
                var response = _repository.DeleteAsync(id);
                return GenericBaseResponse<bool>.Ok(response.IsCompletedSuccessfully);
            }
            catch (Exception ex)
            {
                return GenericBaseResponse<bool>.Error(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<BaseResponse> PatchAsync(int id)
        {
            var response = new BaseResponse();

            try
            {
                await _repository.PatchAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ListErrors.Add(ex.Message);
            }

            return response;
        }

        Task<BaseResponse> IProductService.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
