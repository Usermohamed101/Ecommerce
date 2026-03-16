using Ecommerce.Dtos.Category;
using Ecommerce.Dtos.Product;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService serv) : ControllerBase
    {


        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {

            var res = (await serv.GetAllAsync()).ToList();

            return res.Count > 0 ? Ok(res) : BadRequest();

        }


        [HttpGet("GetCategory/{id:int}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var res = await serv.GetByIdAsync(id);
            return res is null ? BadRequest() : Ok(res);

        }


        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(CreateCategory cat)
        {
            if (!ModelState.IsValid) return BadRequest();
            var res = await serv.Add(cat);
            return res.succeeded ? Ok(res) : BadRequest();

        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategory pro)
        {
            if (!ModelState.IsValid) return BadRequest();
            var res = await serv.Update(pro);
            return res.succeeded ? Ok(res) : BadRequest();

        }
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var res = await serv.Delete(id);
            return res.succeeded ? Ok(res) : BadRequest();

        }


    }
}
