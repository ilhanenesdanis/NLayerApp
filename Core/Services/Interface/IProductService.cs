using Core.DTO;
using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interface
{
    public interface IProductService:IService<Product>
    {
        Task<CustomResponseDto<List<ProductDto>>> GetProductWithCategory();
    }
}
