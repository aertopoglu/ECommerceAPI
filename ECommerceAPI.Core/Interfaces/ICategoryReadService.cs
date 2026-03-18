using ECommerceAPI.Core.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Interfaces
{
    public interface ICategoryReadService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO?> GetCategoryByIdAsync(int id);
        Task<CategoryDTO?> GetCategoryByUrlAsync(string url);
        Task<CategoryWithProductsDTO?> GetCategoryWithProductsAsync(int id);
    }
}
