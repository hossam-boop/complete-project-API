using API01.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API01.Controllers
{

    public class ProductsController : BaseController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        //private readonly IProductRepository _productRepository;

        public ProductsController(
            IGenericRepository<Product> ProductRepository, 
            IGenericRepository<ProductBrand> ProductBrandRepository,
            IGenericRepository<ProductType> ProductTypeRepository,
            IMapper mapper
            )
        {
            //_productRepository = productRepository;
            _productRepository = ProductRepository;
            _productBrandRepository = ProductBrandRepository;
            _productTypeRepository = ProductTypeRepository;
            _mapper = mapper;
        }

        [HttpGet ("GetProducts")]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            var specs = new ProductWithTypeBrandAndSpecifications(productSpecParams);
            var products = await _productRepository.ListAsync(specs);
            var mappedproducts = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            return Ok(mappedproducts);
        }
        
           
            
        
        [HttpGet ("GetProduct")]

        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var specs = new ProductWithTypeBrandAndSpecifications(id);

            var product = await _productRepository.GetEntityWithSpecifications(specs);

            if (product == null)
                return NotFound();
            var mappedproduct = _mapper.Map<ProductDto>(product);


            return Ok(mappedproduct);

        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrand()
        {

            return Ok(await _productBrandRepository.ListAllAsync());
        }


        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductType()
        {

            return Ok(await _productTypeRepository.ListAllAsync());
        }

    }

}
