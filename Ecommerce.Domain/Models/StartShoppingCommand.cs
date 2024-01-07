using System;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Models;
namespace Ecommerce.Domain.Models
{
    public class StartShoppingCommand
    {
        public Client client { get; }
        public List<UnvalidatedProduct> unvalidatedProducts { get; }
        public StartShoppingCommand(Client client, List<UnvalidatedProduct> unvalidatedProducts)
        {
            this.client = client;
            this.unvalidatedProducts = unvalidatedProducts;
        }
    }
}

