using System;
namespace Ecommerce.Domain.Models
{
    public record ValidatedProduct(ProductID productID, string code, int quantity, double price) : IProduct;
}

