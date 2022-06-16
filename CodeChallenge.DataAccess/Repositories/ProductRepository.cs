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
        public async Task<(ICollection<DtoResponseProduct> collection, int total)> FilterAsync(string? filter, int page, int rows)
        {
            //var list = await _context.Set<Product>()
            //    .Where(p => p.Name.Contains(filter ?? string.Empty))
            //    .ProjectTo<DtoResponseProduct>(_mapper.ConfigurationProvider)
            //    .ToListAsync();

            try
            {
                var list = await _context.Set<Product>()
                    .Where(p => p.Name.Contains(filter ?? string.Empty))
                    .Skip((page - 1) * rows)
                    .Take(rows)
                    .OrderByDescending(p => p.Name)
                    //.ProjectTo<DtoResponseProduct>(_mapper.ConfigurationProvider)
                    .Select(x => _mapper.Map<DtoResponseProduct>(x))
                    .ToListAsync();

                var totalCount = await _context.Set<Product>()
                    .Where(p => p.Name.Contains(filter ?? string.Empty))
                    .CountAsync();

                return (list, totalCount);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await _context.Set<Product>()
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

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<Product>()
                .AsTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (entity != null)
            {
                entity.Active = false;
                await _context.SaveChangesAsync();
            }
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
