using Ecommerce.Dtos.Order;
using Ecommerce.Helper;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService serv) : ControllerBase
    {

        [HttpGet("GetMyOrders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var res = (await serv.GetUserAllOrderAsync(User.GetUserId())).ToList();

            return res.Count() > 0 ? Ok(res) : BadRequest();
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(CreateOrder order)
        {
            var res = await serv.CreateOrder(User.GetUserId(),order);

            return res.succeeded ? Ok(res) : BadRequest();
        }

        [HttpDelete("delete-order")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var res = await serv.Delete(id);

            return res.succeeded ? Ok(res) : BadRequest();
        }


        [HttpDelete("cancel-order")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var res = await serv.UpdateOrderStatusAsync(id,OrderStatus.Cancelled);

            return res.succeeded ? Ok(res) : BadRequest();
        }




    }
}
