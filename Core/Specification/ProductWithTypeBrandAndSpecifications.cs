using Core.Entities;

namespace Core.Specification
{
    public class ProductWithTypeBrandAndSpecifications : BaseSpecifications<Product>
    {
        public ProductWithTypeBrandAndSpecifications(ProductSpecParams productSpecParams) 
            : base( product =>
                   (!productSpecParams.BrandID.HasValue || product.ProductBrandId == productSpecParams.BrandID) &&
                   (!productSpecParams.TypeID.HasValue || product.ProductTypeId == productSpecParams.TypeID)
                  )
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);
            
            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch(productSpecParams.Sort)
                {
                    case "PriceAsc":
                        AddOrderby(product => product.Price);
                        break;
                    case "PriceDesc":
                        AddOrderbyDescending(product => product.Price);
                        break;
                    default:
                        AddOrderby(product => product.Name);
                        break;
                }
            }

        }
        public ProductWithTypeBrandAndSpecifications ( int id)
            :base(product=>product.Id== id)
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);
        }
    }
}
