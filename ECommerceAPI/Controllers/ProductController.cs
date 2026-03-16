using ECommerceAPI.Core.DTOs.Product;
using ECommerceAPI.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }


        [HttpGet("home")]
        public async Task<IActionResult> GetHomeProducts()
        {
            var products = await _productService.GetHomeProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdWithCategoriesAsync(id);
            if (product == null) return NotFound("Product not found");
            return Ok(product);
        }

        [HttpGet("url/{url}")]
        public async Task<IActionResult> GetByUrl(string url)
        {
            var product = await _productService.GetProductByUrlAsync(url);
            if (product == null) return NotFound("Product not found");
            return Ok(product);
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword, int page = 1, int pageSize = 12)
        {
            var products = await _productService.GetSearchProductAsync(keyword, page, pageSize);
            var count = await _productService.GetSearchCountAsync(keyword);
            return Ok(new { Products = products, TotalCount = count });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateProductDTO dto)
        {
            await _productService.CreateProductWithCategoriesAsync(dto);
            return Ok("Product added");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateProductDTO dto)
        {
            await _productService.UpdateProductAsync(dto);
            return Ok("Product updated");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok("Product deleted");
        }


    }
}
