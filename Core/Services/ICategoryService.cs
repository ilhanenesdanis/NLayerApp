using Core.DTO;
using Core.Entity;
using Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ICategoryService:IService<CategoryDto>
    {
        Task<CustomResponseDto<CategoryDto>> GetCategoryIdWithProducts(int CategoryId);
    }
}
