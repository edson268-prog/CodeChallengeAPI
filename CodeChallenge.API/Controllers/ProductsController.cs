using AutoMapper;
using AutoMapper.QueryableExtensions;
using CodeChallenge.DataAccess;
using CodeChallenge.Dto.Request;
using CodeChallenge.Dto.Response;
using CodeChallenge.Entities;
using CodeChallenge.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.API.Controllers
{
    [Route(Constants.DefaultRoute)]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly CodeChallengeDbContext _context;

        private readonly IMapper _mapper;

        private readonly IProductService _service;

        public ProductsController(CodeChallengeDbContext context, IMapper mapper, IProductService service)
        {
            //_context = context;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<DtoResponseProduct>), 200)]
        public async Task<IActionResult> Get(string? filter, int page, int rows)
        {
            //var list = await _context.Set<Product>()
            //    .Include(p => p.ProductType) // INCLUYE EL JOIN SIN NECESIDAD DE POSTERIOMENTE HACER LA CONSULTA
            //    .Where(p => p.Name.StartsWith(filter ?? string.Empty))
            //    .Select(p => new
            //    {
            //        p.Id,
            //        p.Name,
            //        p.Description,
            //        Tipo = p.ProductType.Description //Hace el join con tabla de tipos de producto
            //    })
            //    .ToListAsync();

            //USO DE MAPPER
            //var list = await _context.Set<Product>()
            //    .Where(p => p.Name.Contains(filter ?? string.Empty))
            //    .ProjectTo<DtoResponseProduct>(_mapper.ConfigurationProvider)
            //    .ToListAsync();

            //return Ok(list);

            //CON INYECCION DE DEPENDENCIA
            var response = await _service.FilterAsync(filter, page, rows);

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BaseResponseGeneric<ProductType>), 200)]
        [ProducesResponseType(typeof(BaseResponseGeneric<ProductType>), 404)]
        public async Task<IActionResult> Get(int id)
        {
            //var response = new BaseResponseGeneric<Product>();

            //var product = await _context.Set<Product>()
            //    .FindAsync(id);

            //if (product == null)
            //    return NotFound(response);

            //response.ResponseResult = product;
            //response.Success = true;

            //CON INYECCION DE DEPENDENCIA
            var response = await _service.GetByIdAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseGeneric<Product>), 201)]
        [ProducesResponseType(typeof(BaseResponseGeneric<Product>), 400)]
        public async Task<IActionResult> Post(DtoProduct request)
        {
            //var response = new BaseResponseGeneric<int>();

            //try
            //{
            //    var product = new Product
            //    {
            //        Name = request.Name,
            //        Description = request.Description ?? string.Empty,
            //        AgeRestriction = request.AgeRestriction ?? 0,
            //        Company = request.Company,
            //        Price = request.Price,
            //        ProductTypeId = request.ProductTypeId
            //    };

            //    //CON MAPPER
            //    var product = _mapper.Map<Product>(request);

            //    await _context.Set<Product>().AddAsync(product);
            //    await _context.SaveChangesAsync();

            //    response.Success = true;
            //    response.ResponseResult = product.Id;
            //}
            //catch (Exception ex)
            //{
            //    response.Success = false;
            //    response.ListErrors.Add(ex.Message);
            //}

            //CON INYECCION DE DEPENDENCIA
            var response = await _service.CreateAsync(request);

            return Created($"{response.ResponseResult}", response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        public async Task<IActionResult> Put(int id, DtoProduct request)
        {
            //var response = new BaseResponse();
            //try
            //{
            //    //NORMAL
            //    //var entity = await _context.Set<Product>()
            //    //.AsTracking()
            //    //.FirstOrDefaultAsync(p => p.Id == id);


            //    //if (entity != null)
            //    //{
            //    //entity.Name = request.Name;
            //    //entity.Description = request.Description ?? string.Empty;
            //    //entity.AgeRestriction = request.AgeRestriction ?? 0;
            //    //entity.Company = request.Company;
            //    //entity.Price = request.Price;
            //    ////entity.ProductTypeId = request.ProductTypeId;

            //    //await _context.SaveChangesAsync();
            //    //response.Success = true;
            //    //}

            //    //CON MAPPER
            //    var entity = _mapper.Map<Product>(request);
            //    entity.Id = id;
            //    entity.Active = true;

            //    _context.Attach(entity);
            //    _context.Entry(entity).State = EntityState.Modified;

            //    await _context.SaveChangesAsync();
            //    response.Success = true;
            //}
            //catch (Exception ex)
            //{
            //    response.Success = false;
            //    response.ListErrors.Add(ex.Message);
            //}

            //CON INYECCION DE DEPENDENCIA
            var response = await _service.UpdateAsync(id, request);
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            //var response = new BaseResponse();
            //try
            //{
            //    var entity = await _context.Set<Product>()
            //        .AsTracking()
            //        .FirstOrDefaultAsync(p => p.Id == id);

            //    if (entity != null)
            //    {
            //        entity.Active = false;
            //        response.Success = true;
            //        await _context.SaveChangesAsync();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    response.Success = false;
            //    response.ListErrors.Add(ex.Message);
            //}

            //CON INYECCION DE DEPENDENCIA
            var response = await _service.DeleteAsync(id);
            return Ok(response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        public async Task<IActionResult> Patch(int id)
        {
            //var response = new BaseResponse();

            //var entity = await _context
            //    .Set<Product>()
            //    .AsTracking()
            //    .SingleOrDefaultAsync(p => p.Id == id);

            //if (entity != null)
            //{
            //    entity.SoldOut = true;

            //    await _context.SaveChangesAsync();

            //    response.Success = true;
            //    return Ok(response);
            //}

            //return NotFound(response);

            //CON INYECCION DE DEPENDENCIA
            var response = await _service.PatchAsync(id);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }
    }
}
