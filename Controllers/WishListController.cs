using Ecommerce.Helper;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController(IWishListService serv) : ControllerBase
    {

        [HttpGet("GetMyWishList")]
        public async Task<IActionResult> GetMyWishList()
        {
            var res = await serv.GetByUserIdAsync(User.GetUserId());
            return res is not null ? Ok(res) : BadRequest();
        }

        [HttpPut("AddItem/{productId:int}")]
        public async Task<IActionResult> AddItem(int productId )
        {
            var res = await serv.AddItem(User.GetUserId(),productId);
            return res.succeeded ? Ok(res) : BadRequest();
        }

        [HttpPut("DeleteItem/{productId:int}")]
        public async Task<IActionResult> DeleteItem(int productId)
        {
            var res = await serv.DeleteItem(User.GetUserId(), productId);
            return res.succeeded ? Ok(res) : BadRequest();
        }
    }
}
