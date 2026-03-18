using ECommerceAPI.Core.DTOs.Category;
using ECommerceAPI.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ECommerceAPI.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryWriteService _categoryWriteService;
        private readonly ICategoryReadService _categoryReadService;


        public CategoryController(ICategoryWriteService categoryWriteService,ICategoryReadService categoryReadService)
        {
            _categoryWriteService = categoryWriteService;
            _categoryReadService = categoryReadService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryReadService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryReadService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound("Category not found");
            return Ok(category);
        }

        [HttpGet("url/{url}")]
        public async Task<IActionResult> GetByUrl(string url)
        {
            var category = await _categoryReadService.GetCategoryByUrlAsync(url);
            if (category == null) return NotFound("Category not found");
            return Ok(category);
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetWithProducts(int id)
        {
            var category = await _categoryReadService.GetCategoryWithProductsAsync(id);
            if (category == null) return NotFound("Category not found");
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateCategoryDTO categorydto)
        {
            await _categoryWriteService.CreateCategoryAsync(categorydto);
            return Ok("Category created");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateCategoryDTO categorydto)
        {
            await _categoryWriteService.UpdateCategoryAsync(categorydto);
            return Ok("Category updated");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryReadService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound("Category not found");
            await _categoryWriteService.DeleteCategoryAsync(id);
            return Ok("Category deleted");
        }

        [HttpDelete("{productId}/category/{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProductCategory(int productId, int categoryId)
        {
            await _categoryWriteService.DeleteProductCategoryAsync(productId, categoryId);
            return Ok("Product deleted from this category");
        }
    }
}
