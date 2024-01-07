using Domain.Workflow;
using Ecommerce.Data;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Ecommerce.Domain.Models.ShoppingEvent;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly EcommerceAppDbContext dbContext;
        public CartController(EcommerceAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public class PlaceOrderRequest
        {
            [Required]
            public Client client { get; set; }
            [Required]
            public List<UnvalidatedProduct> unvalidatedProducts { get; set; }
        }

        [HttpPost("order")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest request)
        {
            StartShoppingCommand command = new StartShoppingCommand(request.client,request.unvalidatedProducts);
            PlaceOrderWorkflow workflow = new PlaceOrderWorkflow(dbContext);
            IShoppingEvent result = await workflow.execute(command);
            bool succeed = false;
            result.Match(
                whenShoppingFailedEvent: @event =>
                {
                    return @event;
                },
                whenShoppingSucceedEvent: @event =>
                {
                    succeed = true;
                    return @event;
                }
            );
            return succeed ? Ok(result) : BadRequest();
        }

    }
}
