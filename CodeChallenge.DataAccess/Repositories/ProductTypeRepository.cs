﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using CodeChallenge.Dto.Request;
using CodeChallenge.Dto.Response;
using CodeChallenge.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodeChallenge.DataAccess.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly CodeChallengeDbContext _context;
        private readonly IMapper _mapper;

        public ProductTypeRepository(CodeChallengeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //TODO: Im not a big fan of the Repo desing patter with EF Core,
        //still the filter should be an Expression<T> not an string
        //ASNWER: Predicate expression was implemented on Product and ProductType Repositories
        //https://stackoverflow.com/questions/23778903/repository-method-accepting-predicate-and-orderby

        //public async Task<ICollection<DtoResponseProductType>> FilterAsync(string? filter)
        public async Task<ICollection<DtoResponseProductType>> FilterAsync(Expression<Func<ProductType, bool>> expression)
        {
            //var list = await _context.Set<ProductType>()
            //    .Where(p => p.Name.Contains(filter ?? string.Empty))
            //    .ProjectTo<DtoResponseProductType>(_mapper.ConfigurationProvider)
            //    .ToListAsync();

            var list = await _context.Set<ProductType>()
                .Where(expression)
                .ProjectTo<DtoResponseProductType>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return list;
        }

        public async Task<ProductType?> GetByIdAsync(int id)
        {
            var product = await _context.Set<ProductType>()
                .FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public async Task<int> CreateAsync(DtoProductType request)
        {
            var productType = _mapper.Map<ProductType>(request);

            await _context.Set<ProductType>().AddAsync(productType);
            await _context.SaveChangesAsync();

            return productType.Id;
        }

        public async Task UpdateAsync(int id, DtoProductType request)
        {
            var entity = _mapper.Map<ProductType>(request);
            entity.Id = id;
            entity.Active = true;

            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<ProductType>()
                .AsTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (entity != null)
            {
                entity.Active = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
