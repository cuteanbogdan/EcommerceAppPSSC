using Ecommerce.Data.Models;

namespace Ecommerce.Data.Repositories
{
    public class OrderHeadersRepository
    {
        private readonly EcommerceAppDbContext dbContext;

        public OrderHeadersRepository(EcommerceAppDbContext dbContext)
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
            while (await dbContext.OrderHeaders.FindAsync(stringID) != null);

            return stringID;
        }

        public async Task<string> createNewOrderHeader(string clientId, string firstName, string lastName, long phoneNumber, string address, double total, string date)
        {
            OrderHeaderDto orderHeader = new OrderHeaderDto();
            orderHeader.OrderId = await GenerateRandomID();
            orderHeader.ClientId = clientId;
            orderHeader.FirstName = firstName;
            orderHeader.LastName = lastName;
            orderHeader.PhoneNumber = phoneNumber;
            orderHeader.Address = address;
            orderHeader.Date = date;
            orderHeader.Total = total;
            await dbContext.OrderHeaders.AddAsync(orderHeader);
            dbContext.SaveChanges();
            return orderHeader.OrderId;
        }
    }
}

