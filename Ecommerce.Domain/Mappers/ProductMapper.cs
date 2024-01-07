using Ecommerce.Data.Models;
using Ecommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Mappers
{
    public static class ProductMapper
    {
        public static ValidatedProduct MapToValidatedProduct(ProductDto productDto)
        {
            if (productDto == null) { return null; }
            return new ValidatedProduct(new ProductID(productDto.ProductId), productDto.Code, productDto.Stoc, productDto.PricePerPiece);
        }
    }
}
