using AutoMapper;
using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoffeeManagementAPI.DTO;
using CoffeeManagementAPI.Models;

namespace CoffeeManagementAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
            mapper = config.CreateMapper();
        }

        [HttpGet("[action]")]
        //  [Produces("application/xml")]
        public IActionResult GetAllCategory()
        {
            return Ok(categoryRepository.GetCategories().Select(u => mapper.Map<CategoryDTO>(u)));
        }
        [HttpGet("[action]")]
        public IActionResult GetACategory(int Id)
        {
            Category category = null;
            try
            {
                List<Category> categorys = categoryRepository.GetCategories().ToList();
                category = categorys.FirstOrDefault(u => u.Id == Id);
            }
            catch (Exception ex)
            {
                return Conflict("No Category In DB");
            }
            if (category == null)
                return NotFound("Category doesnt exist");
            return Ok(mapper.Map<CategoryDTO>(category));
        }

    }
}
