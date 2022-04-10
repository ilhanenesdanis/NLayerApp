using Core.Entity;
using Core.Repositorys.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(Context context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductWithCategory()
        {
            return await _context.Products.Include(x => x.Category).AsNoTracking().AsSplitQuery().ToListAsync();
        }
    }
}
