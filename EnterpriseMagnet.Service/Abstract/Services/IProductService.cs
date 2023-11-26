using EnterpriseMagnet.Dto.Abtract;
using EnterpriseMagnet.Entities.Concrete;
using EnterpriseMagnet.Service.Concrete.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseMagnet.Service.Abstract.Services
{
    public interface IProductService : IService<Product>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory();
    }
}
