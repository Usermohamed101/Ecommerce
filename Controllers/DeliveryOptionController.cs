using Ecommerce.Dtos.DeliveryOption;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryOptionController(IDeliveryOptionsService serv) : ControllerBase
    {


        [HttpGet("GetDeliveryOptions")]
        public async Task<IActionResult> GetDeliveryOptions()
        {

            var res = (await serv.GetAllAsync()).ToList();
            return res.Count() > 0 ? Ok(res) : BadRequest();
        }


        [HttpGet("GetDeliveryOption/{id:int}")]
        public async Task<IActionResult> GetDeliveryOptions(int optionId)
        {

            var res = await serv.GetByIdAsync(optionId);
            return res !=null ? Ok(res) : BadRequest();
        }


        [HttpPost("AddDeliveryOption")]
        public async Task<IActionResult> AddDeliveryOption(CreateDeliveryOption option)
        {

            var res = await serv.Add(option);
            return res.succeeded  ? Ok(res) : BadRequest();
        }

        [HttpPut("UpdateDeliveryOption")]
        public async Task<IActionResult> UpdateDeliveryOption(UpdateDeliveryOption option)
        {

            var res = await serv.Update(option);
            return res.succeeded ? Ok(res) : BadRequest();
        }


        [HttpPost("DeleteDeliveryOption/{id:int}")]
        public async Task<IActionResult> DeleteDeliveryOption(int optionId)
        {

            var res = await serv.Delete(optionId);
            return res.succeeded ? Ok(res) : BadRequest();
        }


    }
}
