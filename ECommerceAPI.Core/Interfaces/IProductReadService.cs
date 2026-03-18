using ECommerceAPI.Core.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Interfaces
{
    public interface IProductReadService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<IEnumerable<ProductDTO>> GetHomeProductsAsync();
        Task<IEnumerable<ProductDTO>> GetActiveProductsAsync();
        Task<IEnumerable<ProductDTO>> GetProductsWithCategoryAsync();
        Task<IEnumerable<ProductDTO>> GetSearchProductAsync(string keyword, int page, int pageSize);
        Task<int> GetSearchCountAsync(string keyword);
        Task<int> GetCountByCategoryAsync(string url);
        Task<bool> IsInStockAsync(int ProductId);
        Task<ProductDTO?> GetProductByIdWithCategoriesAsync(int id);
        Task<ProductDTO?> GetProductByUrlAsync(string url);
    }
}
