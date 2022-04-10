using AutoMapper;
using Core.DTO;
using Core.Entity;
using Core.Services;
using Core.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IService<Category> _service;

        public CategoryController(ICategoryService categoryService, IMapper mapper, IService<Category> service)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _service = service;
        }
        [HttpGet("GetByCategoryByProduct/{categoryId}")]
        public async Task<IActionResult> GetCategoryWithProducts(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetCategoryIdWithProducts(categoryId));
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
        {
            var Category= await _service.AddAsync(_mapper.Map<Category>(categoryDto));
            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(200));
        }
    }
}
