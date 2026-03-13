using ECommerceAPI.Domain.Entites;
using ECommerceAPI.Domain.Interfaces;
using ECommerceAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task DeleteProductsByCategory(int productId, int categoryId)
        {
            var productCategory = await _context.ProductCategories.FirstOrDefaultAsync(pc => pc.ProductID == productId && pc.CategoryID == categoryId);
            if(productCategory != null)
            {
                _context.Remove(productCategory);
                await _context.SaveChangesAsync();
            }
        }

        

        public async Task<Category?> GetCategoryByUrlAsync(string url)
        {
           return await _context.Categories.FirstOrDefaultAsync(c => c.Url == url);
        }

        public async Task<Category?> GetCategoryWithProducts(int productId, int categoryId)
        {
            return await _context.Categories
                .Include(c => c.ProductCategories)
                .ThenInclude(pc => pc.Product)
                .FirstOrDefaultAsync();
        }
    }
}
