using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(x => x.productType);
            AddInclude(x => x.productBrand);    
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x=> x.Id == id)
        {
            AddInclude(x => x.productType);
            AddInclude(x => x.productBrand);
        }
    }
}
