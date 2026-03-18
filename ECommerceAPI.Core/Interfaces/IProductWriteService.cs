using ECommerceAPI.Core.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Interfaces
{
    public interface IProductWriteService
    {
        Task CreateProductWithCategoriesAsync(CreateProductDTO productdto);
        Task UpdateProductAsync(UpdateProductDTO productdto);
        Task DeleteProductAsync(int ProductId);
        Task UpdateStockAsync(int ProductId, int quantity);
       
    }
}
