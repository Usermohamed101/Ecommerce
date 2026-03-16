using Ecommerce.Dtos.PaymentDetailsDto;
using Ecommerce.Helper;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("payment-window")]
    public class PaymentController(IPaymentDetailsService serv,IOrderService orderServ) : ControllerBase
    {

      

        [HttpGet("GetPaymentDetails")]
        public async Task<IActionResult> GetMyPaymentDetails()
        {

            var res = await serv.GetAllUserPaymentDetails(User.GetUserId());
            return res.Count() > 0 ? Ok(res) : BadRequest();
        }

        [HttpGet("getpaymentMethods")]
        public async Task<IActionResult> GetPaymentMethods()
        {
            return Ok( await serv.GetPaymentMethodsAsync());
        }

        
        [HttpPost("executePayment")]
        public async Task<IActionResult> ExecutePaymentAsync(ExecutePaymentRequest req)
        {
            if ((await orderServ.GetByIdAsync(req.OrdrerId)).Status != OrderStatus.Pending) return Ok("this order can t be  paid");

            var res= await serv.ExecutePaymentAsync(User.GetUserId(), req);
            return Ok(res);
        }





    }
}
