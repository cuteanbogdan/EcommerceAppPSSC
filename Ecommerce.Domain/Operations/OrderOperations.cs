using System;
using Ecommerce.Data;
using Ecommerce.Data.Repositories;

namespace Ecommerce.Domain.Operations
{
    public class OrderOperations
    {
        private readonly EcommerceAppDbContext dbContext;
        ProductRepository productsRepository;
        OrderHeadersRepository orderHeadersRepository;
        OrderLinesRepository orderLinesRepository;

        public OrderOperations(EcommerceAppDbContext dbContext)
        {
            this.dbContext = dbContext;
            productsRepository = new ProductRepository(dbContext);
            orderHeadersRepository = new OrderHeadersRepository(dbContext);
            orderLinesRepository = new OrderLinesRepository(dbContext);
        }

        public async Task<bool> RemoveOrder(string orderId)
        {
            await orderLinesRepository.RemoveEachLine(orderId);
            await orderHeadersRepository.removeOrder(orderId);
            return true;
        }
    }
}

