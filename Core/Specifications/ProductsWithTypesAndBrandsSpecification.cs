using Core.Models;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification (ProductsSpecParams productsParams)
            :base (x =>
                 (string.IsNullOrEmpty(productsParams.Search) || x.Name.ToLower().
                     Contains(productsParams.Search)) &&
                 (!productsParams.TypeId.HasValue || x.ProductTypeId == productsParams.TypeId) && 
                 (!productsParams.BrandId.HasValue || x.ProductBrandId == productsParams.BrandId)
            )
        {
            AddInclude(x => x.productType);
            AddInclude(x => x.productBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productsParams.PageSize * (productsParams.PageIndex - 1)
                ,productsParams.PageSize);

            if(!string.IsNullOrEmpty(productsParams.Sort))
            {
                switch(productsParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x=> x.Id == id)
        {
            AddInclude(x => x.productType);
            AddInclude(x => x.productBrand);
        }
    }
}
