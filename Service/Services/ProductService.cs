using AutoMapper;
using Core.DTO;
using Core.Entity;
using Core.Repositorys.Interface;
using Core.Services.Interface;
using Core.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> genericRepository, IUnitOfWork unitOfWork, IProductRepository repository, IMapper mapper) : base(genericRepository, unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CustomResponseDto<List<ProductDto>>> GetProductWithCategory()
        {
            var product = await _repository.GetProductWithCategory();
            var ProductsDto = _mapper.Map<List<ProductDto>>(product);
            return CustomResponseDto<List<ProductDto>>.Success(200, ProductsDto);
        }
    }
}
