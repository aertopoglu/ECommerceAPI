using ECommerceAPI.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetHomeProductsAsync();
        Task<IEnumerable<Product>> GetActiveProductsAsync();
        Task<IEnumerable<Product>> GetProductsWithCategoryAsync();
        Task<Product?> GetProductDetailsByUrlAsync(string url);
        Task<Product?> GetByIdWithCategoriesAsync(int id);
        Task<IEnumerable<Product>> GetCategoryProductsAsync(string url, int page, int pageSize);
        Task<int> GetCountByCategoryAsync(string url);
        Task<IEnumerable<Product>> GetSearchProductAsync(string keyword, int page, int pageSize);
        Task<int> GetSearchCountAsync(string keyword);
        Task CreateProductsWithCategoriesAsync(Product entity, int[] categoryIds);
        Task UpdateProductCategoriesAsync(int productId, int[] categoryIds);
        Task<bool> IsInStockAsync(int productid);
        Task UpdateStockAsync(int productid,int quantity);


    }
}
