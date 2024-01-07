using System;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Domain.Models
{
    public class CashPayementMethod : IPaymentMethod
    {
        public CashPayementMethod()
        {
            this.name = "Cash";
        }

        public string name { get; }
    }
}

