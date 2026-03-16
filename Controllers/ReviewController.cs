using Ecommerce.Dtos.Review;
using Ecommerce.Helper;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController(IReviewService serv) : ControllerBase
    {

        [HttpPost("add-review")]
        public async Task<IActionResult> AddReview(CreateReview r)
        {

            if(!ModelState.IsValid) return BadRequest(r);

            var res=await serv.Add(r);

            return res.succeeded ? Ok(res) : BadRequest(res.msg);

        }

        [HttpPut("update-review")]
        public async Task<IActionResult> UpdateReview(UpdateReview r)
        {

            if (!ModelState.IsValid) return BadRequest(r);

            var res = await serv.Update(r);

            return res.succeeded ? Ok(res) : BadRequest(res.msg);

        }

        [HttpDelete("delete-review")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {

            if (!ModelState.IsValid) return BadRequest();

            var res = await serv.Delete(reviewId);

            return res.succeeded ? Ok(res) : BadRequest(res.msg);

        }

        [HttpGet("get-all-my-reviews")]
        public async Task<IActionResult> getMyReviews()
        {

        

            var res = await serv.GetReviewsByUserId(User.GetUserId());

            return res is not null ? Ok(res) : BadRequest();

        }

    }
}
