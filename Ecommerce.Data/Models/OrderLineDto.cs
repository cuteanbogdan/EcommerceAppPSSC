namespace Ecommerce.Data.Models
{
    public class OrderLineDto
    {
        public string OrderLineId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public string OrderId { get; set; }

        public string ProductId { get; set; }
    }
}

