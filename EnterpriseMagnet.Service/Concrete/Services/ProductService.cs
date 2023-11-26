using AutoMapper;
using EnterpriseMagnet.DataAccess.Abstract.UnitOfWorks;
using EnterpriseMagnet.Dto.Abtract;
using EnterpriseMagnet.Entities.Concrete;
using EnterpriseMagnet.Repository.Abstract;
using EnterpriseMagnet.Service.Abstract.Services;
using EnterpriseMagnet.Service.Concrete.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseMagnet.Service.Concrete.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> genericRepository, IUnitOfWorks unitOfWorks, IMapper mapper, IProductRepository productRepository) : base(genericRepository, unitOfWorks)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory()
        {
            var products = await _productRepository.GetProductWithCategory();
            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }
    }
}
