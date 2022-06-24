using AutoMapper;
using AutoMapper.QueryableExtensions;
using CodeChallenge.Dto.Request;
using CodeChallenge.Dto.Response;
using CodeChallenge.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodeChallenge.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CodeChallengeDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(CodeChallengeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //public async Task<(ICollection<DtoResponseProduct> collection, int total)> FilterAsync(string? filter, int page, int rows)
        public async Task<(ICollection<DtoResponseProduct> collection, int total)> FilterAsync(Expression<Func<Product, bool>> expression, int page, int rows)
        {
            //var list = await _context.Set<Product>()
            //    .Where(p => p.Name.Contains(filter ?? string.Empty))
            //    .ProjectTo<DtoResponseProduct>(_mapper.ConfigurationProvider)
            //    .ToListAsync();

            try
            {
                //TODO: Why the Skip -1 * Rows?
                //ANSWER: The function performs the calculation of the records that must skip the query to obtain the data of the current
                //page, example if "page" is 1 the result will be 0 because no record should be skipped if "page" is 2,
                //you must skip the number of "rows" equivalent to those of the first page (2-1)*rows
                var list = await _context.Set<Product>()
                    //.Where(p => p.Name.Contains(filter ?? string.Empty))
                    .Where(expression)
                    .Skip((page - 1) * rows)
                    .Take(rows)
                    .OrderByDescending(p => p.Name)
                    //.ProjectTo<DtoResponseProduct>(_mapper.ConfigurationProvider)
                    .Select(x => _mapper.Map<DtoResponseProduct>(x))
                    .ToListAsync();

                var totalCount = await _context.Set<Product>()
                    //.Where(p => p.Name.Contains(filter ?? string.Empty))
                    .Where(expression)
                    .CountAsync();

                return (list, totalCount);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        //Fixed observations db set and set and included table join Product and ProductType
        public async Task<Product?> GetByIdAsync(int id)
        {
            //var product = await _context.Set<Product>()
            var product = await _context.Product // Using DbSet beacuse i already know what class i'm working with
                .Include(p => p.ProductType) // Include "ProductType" Table to join
                .FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public async Task<int> CreateAsync(DtoProduct request)
        {
            var product = _mapper.Map<Product>(request);

            await _context.Set<Product>().AddAsync(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task UpdateAsync(int id, DtoProduct request)
        {
            var entity = _mapper.Map<Product>(request);
            entity.Id = id;
            entity.Active = true;

            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<Product>()
            .AsTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

            if (entity != null)
            {
                entity.Active = false;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task PatchAsync(int id)
        {
            var response = new BaseResponse();

            var entity = await _context
                .Set<Product>()
                .AsTracking()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (entity != null)
            {
                entity.SoldOut = true;

                await _context.SaveChangesAsync();

                response.Success = true;
            }
        }
    }
}
