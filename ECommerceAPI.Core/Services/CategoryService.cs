using AutoMapper;
using ECommerceAPI.Core.DTOs.Category;
using ECommerceAPI.Core.Interfaces;
using ECommerceAPI.Domain.Entites;
using ECommerceAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Services
{
    public class CategoryService : ICategoryWriteService,ICategoryReadService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDTO categorydto)
        {
            var product = _mapper.Map<Category>(categorydto);
            await _categoryRepository.AddAsync(product);
        }

        public async Task DeleteCategoryAsync(int id)
        {
           await _categoryRepository.DeleteAsync(id);
        }

        public async Task DeleteProductCategoryAsync(int productId, int categoryId)
        {
            await _categoryRepository.DeleteProductsByCategory(productId, categoryId);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO?>(category);
        }

        public async Task<CategoryDTO?> GetCategoryByUrlAsync(string url)
        {
            var category = await _categoryRepository.GetCategoryByUrlAsync(url);
            return _mapper.Map<CategoryDTO?>(category);
        }

        public async Task<CategoryWithProductsDTO?> GetCategoryWithProductsAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryWithProducts(id);
            return _mapper.Map<CategoryWithProductsDTO?>(category);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTO categorydto)
        {
            var category = _mapper.Map<Category>(categorydto);
            await _categoryRepository.UpdateAsync(category);
        }
    }
}
