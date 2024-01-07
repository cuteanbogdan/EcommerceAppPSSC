using System;
using CSharp.Choices;
using Ecommerce.Domain.Interfaces;


namespace Ecommerce.Domain.Models
{
    [AsChoice]
    public static partial class PaymentEvent
    {
        public interface IPaymentEvent { }

        public record PaymentSucceedEvent : IPaymentEvent
        {
            public IPaymentMethod paymentMethod;

            public PaymentSucceedEvent(IPaymentMethod paymentMethod)
            {
                this.paymentMethod = paymentMethod;
            }
        }

        public record PaymentFaileddEvent : IPaymentEvent
        {
            public string error;

            public PaymentFaileddEvent(string error)
            {
                this.error = error;
            }
        }
    }
}

