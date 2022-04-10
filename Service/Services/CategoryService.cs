using AutoMapper;
using Core.DTO;
using Core.Repositorys.Interface;
using Core.Services;
using Core.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CategoryService : Service<CategoryDto>, ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryService(IGenericRepository<CategoryDto> genericRepository, IUnitOfWork unitOfWork, ICategoryRepository repository, IMapper mapper) : base(genericRepository, unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<CategoryDto>> GetCategoryIdWithProducts(int CategoryId)
        {
            var CategoryWidhProduct = await _repository.GetCategoryIdWidthProducts(CategoryId);
            var category = _mapper.Map<CategoryDto>(CategoryWidhProduct);
           return CustomResponseDto<CategoryDto>.Success(200, category);
        }
    }
}
