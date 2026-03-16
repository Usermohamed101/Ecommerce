using Ecommerce.Dtos.Product;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService serv) : ControllerBase
    {



        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {

          var res=(await  serv.GetAllAsync()).ToList();

            return res.Count > 0 ? Ok(res) : BadRequest();

        }


        [HttpGet("GetProduct/{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var res=await serv.GetByIdAsync(id);
            return res is null ? BadRequest() : Ok(res);

        }


        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(CreateProduct pro)
        {
            if (!ModelState.IsValid) return BadRequest();
            var res = await serv.Add(pro);
            return res.succeeded ? Ok(res) :BadRequest();

        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm]UpdateProduct pro)
        {
            if (!ModelState.IsValid) return BadRequest($"the model state not valid");
            var res = await serv.Update(pro);
            return res.succeeded ? Ok(res) : BadRequest();

        }
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct( int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var res = await serv.Delete(id);
            return res.succeeded ? Ok(res) : BadRequest();

        }









    }
}
