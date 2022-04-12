using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification :BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductsSpecParams productsParams)
            : base(x =>
                 (string.IsNullOrEmpty(productsParams.Search) || x.Name.ToLower().
                     Contains(productsParams.Search)) &&
                 (!productsParams.TypeId.HasValue || x.ProductTypeId == productsParams.TypeId) &&
                 (!productsParams.BrandId.HasValue || x.ProductBrandId == productsParams.BrandId)
            )
        {

        }
    }
}
