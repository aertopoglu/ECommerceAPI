using ECommerceAPI.Core.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Interfaces
{
    public interface ICategoryWriteService
    {
        Task CreateCategoryAsync(CreateCategoryDTO categorydto);
        Task UpdateCategoryAsync(UpdateCategoryDTO categorydto);
        Task DeleteCategoryAsync(int id);
        Task DeleteProductCategoryAsync(int productId, int categoryId);

    }
}
