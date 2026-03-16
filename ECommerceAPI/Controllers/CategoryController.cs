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
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound("Category not found");
            return Ok(category);
        }

        [HttpGet("url/{url}")]
        public async Task<IActionResult> GetByUrl(string url)
        {
            var category = await _categoryService.GetCategoryByUrlAsync(url);
            if (category == null) return NotFound("Category not found");
            return Ok(category);
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetWithProducts(int id)
        {
            var category = await _categoryService.GetCategoryWithProductsAsync(id);
            if (category == null) return NotFound("Category not found");
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateCategoryDTO categorydto)
        {
            await _categoryService.CreateCategoryAsync(categorydto);
            return Ok("Category created");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateCategoryDTO categorydto)
        {
            await _categoryService.UpdateCategoryAsync(categorydto);
            return Ok("Category updated");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound("Category not found");
            await _categoryService.DeleteCategoryAsync(id);
            return Ok("Category deleted");
        }

        [HttpDelete("{productId}/category/{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProductCategory(int productId, int categoryId)
        {
            await _categoryService.DeleteProductCategoryAsync(productId, categoryId);
            return Ok("Product deleted from this category");
        }
    }
}
