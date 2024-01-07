using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Domain.Workflow;



namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly EcommerceAppDbContext dbContext;
        public OrderController(EcommerceAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public class CancelOrderRequest
        {
            [Required]
            public string? orderId { get; set; }
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelOrder([FromBody] CancelOrderRequest request)
        {
            CancelOrderWorkflow workflow = new(dbContext);
            await workflow.execute(request.orderId!);
            return Ok();
        }
    }
}

