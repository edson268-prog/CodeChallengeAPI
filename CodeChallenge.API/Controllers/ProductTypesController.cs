using CodeChallenge.DataAccess;
using CodeChallenge.Dto.Request;
using CodeChallenge.Dto.Response;
using CodeChallenge.Entities;
using CodeChallenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.API.Controllers
{
    [Route(Constants.DefaultRoute)]
    [ApiController]
    public class ProductTypesController : Controller
    {
        private readonly CodeChallengeDbContext _context;
        private readonly IProductTypeService _service;

        public ProductTypesController(CodeChallengeDbContext context, IProductTypeService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<ProductType>), 200)]
        public async Task<IActionResult> Get(string? filter)
        {
            //return Ok(await _context.Set<ProductType>()
            //    .Where( p => p.Description.StartsWith(filter ?? string.Empty))
            //    .IgnoreQueryFilters()
            //    .Select(p => new
            //    {
            //        p.Id,
            //        p.Name,
            //        p.Description
            //    })
            //    .ToListAsync());

            var response = await _service.FilterAsync(filter);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BaseResponseGeneric<ProductType>), 200)]
        [ProducesResponseType(typeof(BaseResponseGeneric<ProductType>), 404)]
        public async Task<IActionResult> Get(int id)
        {
            //var response = new BaseResponseGeneric<ProductType>();

            //var productType = await _context.Set<ProductType>()
            //    .FirstOrDefaultAsync(p => p.Id == id);

            //if (productType == null)
            //    return NotFound(response);

            //response.ResponseResult = productType;
            //response.Success = true;

            //return Ok(response);

            var response = await _service.GetByIdAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseGeneric<ProductType>), 201)]
        [ProducesResponseType(typeof(BaseResponseGeneric<ProductType>), 400)]
        public async Task<IActionResult> Post(DtoProductType request)
        {
            //var response = new BaseResponseGeneric<int>();

            //try
            //{
            //    var productType = new ProductType
            //    {
            //        Name = request.Name ?? string.Empty,
            //        Description = request.Description ?? string.Empty,
            //    };

            //    await _context.Set<ProductType>().AddAsync(productType);
            //    await _context.SaveChangesAsync();

            //    response.Success = true;
            //    response.ResponseResult = productType.Id;
            //}
            //catch (Exception ex)
            //{
            //    response.Success = false;
            //    response.ListErrors.Add(ex.Message);
            //}

            //return Created($"{response.ResponseResult}", response);

            var response = await _service.CreateAsync(request);
            return Created($"{response.ResponseResult}", response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        public async Task<IActionResult> Put(int id, DtoProductType request)
        {
            //var response = new BaseResponse();
            //try
            //{
            //    var entity = await _context.Set<ProductType>()
            //        .AsTracking()
            //        .FirstOrDefaultAsync(p => p.Id == id);

            //    if (entity != null)
            //    {
            //        entity.Name = request.Name ?? string.Empty;
            //        entity.Description = request.Description ?? string.Empty;
            //        response.Success = true;
            //        await _context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    response.Success = false;
            //    response.ListErrors.Add(ex.Message);
            //}

            //return Ok(response);

            var response = await _service.UpdateAsync(id, request);
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new BaseResponse();
            try
            {
                var entity = await _context.Set<ProductType>()
                    .AsTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (entity != null)
                {
                    entity.Active = false;
                    response.Success = true;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ListErrors.Add(ex.Message);
            }

            return Ok(response);
        }
    }
}
