using AutoMapper;
using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoffeeManagementAPI.DTO;
using CoffeeManagementAPI.Models;
using System.Text.Json;

namespace CoffeeManagementAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository productRepository;
        private readonly IMapper mapper;
        MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            mapper = config.CreateMapper();
        }

        [HttpGet("[action]")]
        //   [Produces("application/xml")]
        public IActionResult GetAllProduct()
        {
            return Ok(productRepository.GetProducts().Select(u => mapper.Map<ProductDTO>(u)));
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Search(string name) => productRepository.GetProducts().Where(c => c.Name.ToLower().Contains(name.Trim().ToLower())).ToList();
        [HttpGet("[action]")]
        public IActionResult GetAProduct(int Id)
        {
            Product product = null;
            try
            {
                List<Product> products = productRepository.GetProducts().ToList();
                product = products.FirstOrDefault(u => u.Id == Id);
            }
            catch (Exception ex)
            {
                return Conflict("No Product In DB");
            }
            if (product == null)
                return NotFound("Product doesnt exist");
            return Ok(mapper.Map<ProductDTO>(product));
        }

        [HttpGet("[action]/{productDTO}")]
        public IActionResult AddAProduct(string productDTO)
        {
            ProductDTO prod = JsonSerializer.Deserialize<ProductDTO>(productDTO);
            try
            {
                Product product = mapper.Map<Product>(prod);
                productRepository.SaveProduct(product);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(prod);

        }
        [HttpGet("[action]/{productDTO}")]
        public IActionResult UpdateAProduct(string productDTO)
        {
            ProductDTO prod = JsonSerializer.Deserialize<ProductDTO>(productDTO);
            try
            {
                Product product = mapper.Map<Product>(prod);
                productRepository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(prod);

        }
        [HttpDelete("[action]")]
        public IActionResult DeleteAProduct(int Id)
        {
            Product product = null;
            try
            {
                List<Product> products = productRepository.GetProducts().ToList();
                product = products.FirstOrDefault(u => u.Id == Id);
                if (product == null)
                    return NotFound("Product doesnt exist");
                productRepository.DeleteProduct(product);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(productRepository.GetProducts());
        }
    }
}
