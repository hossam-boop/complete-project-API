using API01.Dtos;
using AutoMapper;
using Core.Entities;

namespace API01.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl)) 
            { 
                return _configuration["APIUrl"]+source.PictureUrl;
            }
            return null;
        }
    }
}
