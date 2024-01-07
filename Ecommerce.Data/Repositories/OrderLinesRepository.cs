using System;
using Ecommerce.Data;
using Ecommerce.Data.Models;

namespace Ecommerce.Data.Repositories
{
    public class OrderLinesRepository
    {
        private readonly EcommerceAppDbContext dbContext;

        public OrderLinesRepository(EcommerceAppDbContext dbContext)
        {
            this.dbContext = dbContext;
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
    }
}

