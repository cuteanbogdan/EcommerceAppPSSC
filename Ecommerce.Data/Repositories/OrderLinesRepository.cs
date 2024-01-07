using System;
using Ecommerce.Data;
using Ecommerce.Data.Models;

namespace Ecommerce.Data.Repositories
{
    public class OrderLinesRepository
    {
        private readonly EcommerceAppDbContext dbContext;
        private ProductRepository productRepository;
        public OrderLinesRepository(EcommerceAppDbContext dbContext)
        {
            this.dbContext = dbContext;
            productRepository = new(new EcommerceAppDbContext());
        }

        public async Task<string> GenerateRandomID()
        {
            string stringID;
            do
            {
                stringID = Guid.NewGuid().ToString();
            }
            while (await dbContext.OrderLines.FindAsync(stringID) != null);

            return stringID;
        }


        public async Task AddProductLine(string productId, int quantity, double price, string orderID)
        {
            OrderLineDto orderLine = new OrderLineDto();
            orderLine.OrderId = orderID;
            orderLine.ProductId = productId;
            orderLine.Quantity = quantity;
            orderLine.Price = price;
            orderLine.OrderLineId = await GenerateRandomID();
            await dbContext.OrderLines.AddAsync(orderLine);
            dbContext.SaveChanges();
        }

        public async Task RemoveEachLine(string orderId)
        {
            List<OrderLineDto> orderLines = dbContext.OrderLines.Where(o => o.OrderId == orderId).ToList();
            foreach (var orderLine in orderLines)
            {
                productRepository.AddBackQuantity(orderLine.ProductId, orderLine.Quantity);
                dbContext.OrderLines.Remove(orderLine);
            }

            dbContext.SaveChanges();
        }
    }
}

