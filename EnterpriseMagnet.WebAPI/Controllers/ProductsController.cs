using AutoMapper;
using EnterpriseMagnet.Dto.Concrete;
using EnterpriseMagnet.Entities.Concrete;
using EnterpriseMagnet.Service.Abstract.Services;
using EnterpriseMagnet.Service.Concrete.Mapping;
using EnterpriseMagnet.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseMagnet.WebAPI.Controllers
{ 
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Product> _service;
        private readonly IProductService _productService;
        public ProductsController(IMapper mapper, IService<Product> service, IProductService productService = null)
        {
            _mapper = mapper;
            _service = service;
            _productService = productService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProducsWithCategory()
        {
            return CreateActionResult(await _productService.GetProductWithCategory());
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var product = await _service.GetAllAsync();
            var productsDto = _mapper.Map<List<ProductDto>>(product.ToList());
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.DeleteAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
