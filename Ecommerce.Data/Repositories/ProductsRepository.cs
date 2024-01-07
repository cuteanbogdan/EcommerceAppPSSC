using System;
using Ecommerce.Data.Models;

namespace Ecommerce.Data.Repositories
{
    public class ProductRepository
    {
        private readonly EcommerceAppDbContext dbContext;

        public ProductRepository(EcommerceAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProductDto> TryGetProduct(string searchedProduct, int quantity)
        {
            var product = await dbContext.Products.FindAsync(searchedProduct);
            if (product != null && product.Stoc >= quantity)
            {
                return product;
            }

            return null;
        }

        public async Task TryRemoveStoc(string productId, int quantity)
        {
            var product = await dbContext.Products.FindAsync(productId);
            if (product != null)
            {
                product.Stoc = product.Stoc - quantity;
                dbContext.SaveChanges();
            }
        }
    }
}

