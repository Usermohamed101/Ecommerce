using Ecommerce.Dtos.Fawaterak;
using Ecommerce.Dtos.Fawaterak.CallBacks;
using Ecommerce.Helper;
using Ecommerce.Models;
using Ecommerce.Repository;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Ecommerce.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class FawaterakWebHookController(IFawaterakService paymentService,IOrderRepo orderRepo) : ControllerBase
    {







        [HttpPost("paid_json")]
        public async Task< IActionResult> WebHookPaid([FromBody]PaidWebHook dto)//
        {
            var res = paymentService.verifyWebHook(dto);
            var result = 0;
            if (res)
            {

                dto.payloadObject =  JsonSerializer.Deserialize<payLoad>(dto.payLoad);

                var order = await orderRepo.GetByIdAsync(dto.payloadObject.orderId);
                order.Status = OrderStatus.Paid;
                orderRepo.Update(order);
                result = await orderRepo.SaveChangesAsync();


            }
            return result > 0 ? Ok("payment process is complete") : BadRequest("something went wrong while processing payment");

           

           
           

        }

        [HttpPost("cancel_json")]
        public IActionResult WebHookcancel([FromBody] CancelWebHook dto)
        {
            var res = paymentService.verifyWebHookCancelTransaction(dto);
            return res ? Ok("payment process is cancelled") : BadRequest("something went wrong while cancelling payment process");

        }
    }
}
