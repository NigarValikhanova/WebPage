using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.DAL;
using ProductAPI.DTO;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet(Name = "CategoryController")]
        public IActionResult Get()
        {
            var categories = _appDbContext.Categories.Include(p => p.Products).ToList();
            return Ok(categories);
        }
        [HttpGet("{id}/{take}")]
        public async Task<IActionResult> Get(int id, int take)
        {
            var data = await _appDbContext.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            var product = data.Products.Take(take);

            var productDto = product.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            });
            return Ok(productDto);
        }
    }
}
