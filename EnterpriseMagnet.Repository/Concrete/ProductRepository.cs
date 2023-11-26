using EnterpriseMagnet.Entities.Concrete;
using EnterpriseMagnet.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseMagnet.Repository.Concrete
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(EnterpriseMagnetDbContext appContext) : base(appContext)
        {
        }

        public async Task<List<Product>> GetProductWithCategory()
        {
            return await _appContext.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
