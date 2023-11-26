using AutoMapper;
using EnterpriseMagnet.DataAccess.Abstract.UnitOfWorks;
using EnterpriseMagnet.Dto.Abtract;
using EnterpriseMagnet.Entities.Concrete;
using EnterpriseMagnet.Repository.Abstract;
using EnterpriseMagnet.Repository.Concrete;
using EnterpriseMagnet.Service.Abstract.Services;
using EnterpriseMagnet.Service.Concrete.Exceptions;
using EnterpriseMagnet.Service.Concrete.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseMagnet.Caching.Concrete
{
    public class ProductServiceWithCaching : IProductService
    {

        private const string CacheProductKey = "productCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWorks _unitOfWorks;

        public ProductServiceWithCaching(IUnitOfWorks unitOfWorks, IProductRepository productRepository, IMemoryCache memoryCache, IMapper mapper)
        {
            _unitOfWorks = unitOfWorks;
            _productRepository = productRepository;
            _memoryCache = memoryCache;
            _mapper = mapper;

            if (!_memoryCache.TryGetValue(CacheProductKey, out _))
            {
                _memoryCache.Set(CacheProductKey, _productRepository.GetAll().ToList());

            }

        }



        public async Task<bool> AddAsync(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _productRepository.AddAsync(entity);
            await _unitOfWorks.CommitAsync();
            await CacheAllProduct();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _productRepository.AddRangeAsync(entities);
            await _unitOfWorks.CommitAsync();
            await CacheAllProduct();
            return entities;
        }

        public async Task DeleteAsync(Product entity)
        {
            _productRepository.Delete(entity);
            await _unitOfWorks.CommitAsync();
            await CacheAllProduct();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey)); 
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(c => c.Id == id);
            if (product == null)
            { throw new NotFoundException($"{typeof(Product).Name} not found"); }

            return Task.FromResult(product);
        }

        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Product entity)
        {
            _productRepository.Update(entity);
            await _unitOfWorks.CommitAsync();
            await CacheAllProduct();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task CacheAllProduct()
        {
            _memoryCache.Set(CacheProductKey, _productRepository.GetAll().ToListAsync());
        }
    }
}
