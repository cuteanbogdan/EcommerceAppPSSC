namespace Data.Models
{
    public class OrderHeaderDto
    {
        public string OrderId { get; set; }
        public string ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }
        public double Total { get; set; }
    }
}

