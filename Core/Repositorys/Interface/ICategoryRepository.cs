using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositorys.Interface
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Task<Category> GetCategoryIdWidthProducts(int categoryId);
    }
}
