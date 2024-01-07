using System;
namespace Ecommerce.Domain.Models
{
    public record UnvalidatedProduct(string productId, int quantity) : IProduct;
}

