using Ecommerce.Domain.Models;
using static Ecommerce.Domain.Models.Cart;
using static Ecommerce.Domain.Models.PaymentEvent;
using Ecommerce.Data;
using Ecommerce.Data.Repositories;
using Ecommerce.Domain.Mappers;
using Ecommerce.Data.Models;

namespace Ecommerce.Domain.Operations
{
    public class CartOperation
    {
        private readonly EcommerceAppDbContext dbContext;
        ProductRepository productsRepository;
        OrderHeadersRepository orderHeadersRepository;
        OrderLinesRepository orderLinesRepository;

        public CartOperation(EcommerceAppDbContext dbContext)
        {
            this.dbContext = dbContext;
            productsRepository = new ProductRepository(dbContext);
            orderHeadersRepository = new OrderHeadersRepository(dbContext);
            orderLinesRepository = new OrderLinesRepository(dbContext);
        }
        public static ICart addProductToCart(EmptyCart emptyCart, IReadOnlyCollection<UnvalidatedProduct> unvalidatedProducts)
        {
            return unvalidatedProducts.Count > 0 ? new UnvalidatedCart(emptyCart.client, unvalidatedProducts) : emptyCart;
        }

        public async Task<ICart> validateCart(UnvalidatedCart unvalidatedCart)
        {
            bool isValid = true;
            List<ValidatedProduct> cartProducts = new List<ValidatedProduct>();
            foreach (var product in unvalidatedCart.products)
            {
                ValidatedProduct tempValidProduct = ProductMapper.MapToValidatedProduct(await productsRepository.TryGetProduct(product.productId, product.quantity));

                ValidatedProduct validProduct = new(tempValidProduct.productID, tempValidProduct.code, product.quantity, tempValidProduct.price * product.quantity);
                if (validProduct != null)
                {
                    cartProducts.Add(validProduct);
                }
                else
                {
                    isValid = false;
                    break;
                }
            }

            return isValid ? new ValidatedCart(unvalidatedCart.client, cartProducts) : unvalidatedCart;
        }

        public static CalculatedCart calculateCart(ValidatedCart validatedCart)
        {
            double total = validatedCart.products.Sum(product => product.price);

            return new CalculatedCart(validatedCart.client, validatedCart.products, total);
        }

        public static ICart payCart(CalculatedCart calculatedCart)
        {
            bool payed = true;
            return payed ? new PaidCart(calculatedCart.client, calculatedCart.products, calculatedCart.price, DateTime.Now) : calculatedCart;
        }

        public async Task sendCart(PaidCart cart)
        {
            string orderId = await orderHeadersRepository.createNewOrderHeader(cart.client.clientId.ToString(),cart.client.prenume,cart.client.nume,cart.client.numarDeTelefon,cart.client.adresa, cart.finalPrice, cart.data.ToString());
            foreach (var product in cart.products)
            {
                await productsRepository.TryRemoveStoc(product.productID.Value, product.quantity);
                await orderLinesRepository.AddProductLine(product.productID.Value, product.quantity, product.price, orderId);
            }
        }
    }
}

