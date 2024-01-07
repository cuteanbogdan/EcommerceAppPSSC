using System;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Domain.Models
{
    public class PaymentCommand
    {
        public double price { get; }
        public PaymentCommand(double price)
        {
            this.price = price;
        }
    }
}

