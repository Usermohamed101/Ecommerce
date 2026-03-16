using Ecommerce.Dtos.CartItem;
using Ecommerce.Dtos.Product;
using Ecommerce.Helper;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService serv) : ControllerBase
    {

        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCart()
        {

            var res = (await serv.GetByUserIdAsync(User.GetUserId()));

            return res is not null  ? Ok(res) : BadRequest();

           

        }


        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem(CreateCartItem item)
        {


            if (!ModelState.IsValid) return BadRequest();
           var res= await serv.AddItemAsync(User.GetUserId(),item);
            return res.succeeded ? Ok(res):BadRequest()  ;

        }


        [HttpPut("UpdateItem")]
        public async Task<IActionResult> UpdateCartItem(UpdateCartItem item)
        {
            if (!ModelState.IsValid) return BadRequest();
            var res = await serv.UpdateItemAsync(User.GetUserId(),item);
            return res.succeeded ? Ok(res) : BadRequest();

        }

        [HttpDelete("DeleteItem")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var res = await serv.DeleteItemAsync(User.GetUserId(),id);
            return res.succeeded ? Ok(res) : BadRequest();

        }


    }
}
