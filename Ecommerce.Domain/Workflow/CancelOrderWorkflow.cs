using System;
using Ecommerce.Data;
using Ecommerce.Domain.Models;
using Ecommerce.Domain.Operations;
using static Ecommerce.Domain.Models.ShoppingEvent;

namespace Ecommerce.Domain.Workflow
{
    public class CancelOrderWorkflow
    {
        private readonly EcommerceAppDbContext dbContext;
        private readonly OrderOperations orderOperation;

        public CancelOrderWorkflow(EcommerceAppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.orderOperation = new OrderOperations(dbContext);
        }

        public async Task<bool> execute(string orderId)
        {
            await orderOperation.RemoveOrder(orderId);
            return true;
        }
    }
}

