using System;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Domain.Models
{
    public class CardPaymentMethod : IPaymentMethod
    {
        public CardPaymentMethod(long cardNumber, int CVV, string expirationDate)
        {
            this.name = "Card";
            this.cardNumber = cardNumber;
            this.CVV = CVV;
            this.expirationDate = expirationDate;
        }

        public string name { get; }
        public long cardNumber;
        public int CVV;
        public string expirationDate;
    }
}

