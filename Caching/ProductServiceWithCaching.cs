using AutoMapper;
using Core.DTO;
using Core.Entity;
using Core.Repositorys.Interface;
using Core.Services.Interface;
using Core.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caching
{
    public class ProductServiceWithCaching : IProductService
    {
        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IUnitOfWork unitOfWork, IProductRepository productRepository, IMemoryCache memoryCache, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _memoryCache = memoryCache;
            _mapper = mapper;

            if (!_memoryCache.TryGetValue(CacheProductKey, out _))
            {
                var list = _productRepository.GetProductWithCategory().Result;
                _memoryCache.Set(CacheProductKey, list);
            }
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _productRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProducts();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _productRepository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProducts();
            return entities;
        }

        public Task<bool> AnyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAll()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
        }

        public IQueryable<Product> GetBy(Expression<Func<Product, bool>> predicate)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(predicate.Compile()).AsQueryable();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name} ({id}) not found");
            }
            return Task.FromResult(product);
        }

        public Task<CustomResponseDto<List<ProductDto>>> GetProductWithCategory()
        {
            var products = _memoryCache.Get<IEnumerable<ProductDto>>(CacheProductKey);
            var ProductWithCateory = _mapper.Map<List<ProductDto>>(products);
            return Task.FromResult(CustomResponseDto<List<ProductDto>>.Success(200, ProductWithCateory));
        }

        public async Task RemoveAsync(Product entity)
        {
            _productRepository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProducts();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entity)
        {
            _productRepository.RemoveRange(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProducts();
        }

        public async Task UpdateAsync(Product entity)
        {
            _productRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProducts();
        }
        public async Task CacheAllProducts()
        {
            _memoryCache.Set(CacheProductKey, await _productRepository.GetAll().ToListAsync());
        }
    }
}
