using System;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Models;
using Ecommerce.Data;
using static Ecommerce.Domain.Models.Cart;
using static Ecommerce.Domain.Models.ShoppingEvent;
using Ecommerce.Domain.Operations;

namespace Domain.Workflow
{
    public class PlaceOrderWorkflow
    {
        private readonly EcommerceAppDbContext dbContext;
        private readonly CartOperation cartOperation;

        public PlaceOrderWorkflow(EcommerceAppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.cartOperation = new CartOperation(dbContext);
        }

        public async Task<IShoppingEvent> execute(StartShoppingCommand command)
        {
            // Initialize an empty cart with the client information
            ICart cart = new EmptyCart(command.client);

            // Add products to the cart
            cart = CartOperation.addProductToCart((EmptyCart)cart, command.unvalidatedProducts);

            // Validate the cart
            if (cart is UnvalidatedCart unvalidatedCart)
            {
                cart = await cartOperation.validateCart(unvalidatedCart);
            }

            // Handle different cart states
            return cart switch
            {
                EmptyCart => new ShoppingFailedEvent("Cart is empty"),
                UnvalidatedCart => new ShoppingFailedEvent("Cart is unvalidated"),
                ValidatedCart validatedCart =>
                    await ProcessValidatedCart(validatedCart),
                CalculatedCart calculatedCart =>
                    await ProcessCalculatedCart(calculatedCart),
                PaidCart paidCart => new ShoppingSucceedEvent(paidCart.data, paidCart.finalPrice),
                _ => new ShoppingFailedEvent("Unknown error occurred")
            };
        }

        private async Task<IShoppingEvent> ProcessValidatedCart(ValidatedCart validatedCart)
        {
            var calculatedCart = CartOperation.calculateCart(validatedCart);

            if (calculatedCart is CalculatedCart)
            {
                return await ProcessCalculatedCart(calculatedCart);
            }

            return new ShoppingFailedEvent("Cart is validated but calculation failed");
        }

        private async Task<IShoppingEvent> ProcessCalculatedCart(CalculatedCart calculatedCart)
        {
            var paidCart = CartOperation.payCart(calculatedCart);

            if (paidCart is PaidCart)
            {
                PaidCart newPaidCart = (PaidCart)paidCart;
                await cartOperation.sendCart(newPaidCart);
                return new ShoppingSucceedEvent(newPaidCart.data, newPaidCart.finalPrice);
            }

            return new ShoppingFailedEvent("Cart is calculated but payment failed");
        }
    }
}
