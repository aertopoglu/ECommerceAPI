using ECommerceAPI.Domain.Entites;
using ECommerceAPI.Domain.Interfaces;
using ECommerceAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
      
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task CreateProductsWithCategoriesAsync(Product entity, int[] categoryIds)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();

            foreach(var categoryId in categoryIds)  
            {
                await _context.ProductCategories.AddAsync(new ProductCategory { ProductID = entity.ProductID, CategoryID = categoryId });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            return await _context.Products.Where(p => p.IsApproved).ToListAsync();
        }


        public async Task<Product?> GetByIdWithCategoriesAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.ProductID==id);
        }

        public async Task<IEnumerable<Product>> GetCategoryProductsAsync(string url, int page, int pageSize)
        {
           return await _context.Products
                 .Where(p => p.ProductCategories
                 .Any(pc => pc.Category.Url == url) && p.IsApproved)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToListAsync();
        }

        public async Task<int> GetCountByCategoryAsync(string url)
        {
            return await _context.Products.Where(p => p.ProductCategories.Any(pc=>pc.Category.Url==url)).CountAsync();  
        }

        public async Task<IEnumerable<Product>> GetHomeProductsAsync()
        {
            return await _context.Products.Where(p=>p.IsApproved && p.IsHome).ToListAsync();
        }

        public async Task<Product?> GetProductDetailsByUrlAsync(string url)
        {
            return await _context.Products
               .Include(p => p.ProductCategories)
               .ThenInclude(pc => pc.Category)
               .FirstOrDefaultAsync(p => p.URL == url);
        }

        public async Task<IEnumerable<Product>> GetProductsWithCategoryAsync()
        {
            return await _context.Products.Include(p => p.ProductCategories)
                           .ThenInclude(pc => pc.Category)
                           .ToListAsync();
        }

        public async Task<int> GetSearchCountAsync(string keyword)
        {
            return await _context.Products.Where(p => p.ProductName.Contains(keyword)).CountAsync();
        }

        public async Task<IEnumerable<Product>> GetSearchProductAsync(string keyword, int page, int pageSize)
        {
            return await _context.Products.Where(p => p.ProductName.Contains(keyword)).Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }

        public async Task<bool> IsInStockAsync(int productid)
        {
            var product = await _context.Products.FindAsync(productid);
            return product != null && product.Stock > 0;
        }

        public async Task UpdateProductCategoriesAsync(int productId, int[] categoryIds)
        {
            var existingcategoryids = _context.ProductCategories.Where(pc => pc.ProductID == productId);
            _context.ProductCategories.RemoveRange(existingcategoryids);
            
            foreach(var categoryId in categoryIds)
            {
                await _context.ProductCategories.AddAsync(new ProductCategory { ProductID = productId, CategoryID = categoryId });
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStockAsync(int productid, int quantity)
        {
            var product = await _context.Products.FindAsync(productid);
            if (product != null)
            {
                product.Stock = product.Stock - quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
