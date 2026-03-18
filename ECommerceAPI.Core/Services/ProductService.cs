using AutoMapper;
using ECommerceAPI.Core.DTOs.Product;
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
    public class ProductService : IProductWriteService,IProductReadService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task CreateProductWithCategoriesAsync(CreateProductDTO productdto)
        {
            var product = _mapper.Map<Product>(productdto);
            await _productRepository.CreateProductsWithCategoriesAsync(product,productdto.CategoryIDs);
        }

        public async Task DeleteProductAsync(int ProductId)
        {
           await _productRepository.DeleteAsync(ProductId);
        }

        public async Task<IEnumerable<ProductDTO>> GetActiveProductsAsync()
        {
            var products = await _productRepository.GetActiveProductsAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<int> GetCountByCategoryAsync(string url)
        {
            return await _productRepository.GetCountByCategoryAsync(url);
        }

        public async Task<IEnumerable<ProductDTO>> GetHomeProductsAsync()
        {
            var products = await _productRepository.GetHomeProductsAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO?> GetProductByIdWithCategoriesAsync(int id)
        {
            var product = await _productRepository.GetByIdWithCategoriesAsync(id);
            return _mapper.Map<ProductDTO?>(product);
        }

        public async Task<ProductDTO?> GetProductByUrlAsync(string url)
        {
            var product = await _productRepository.GetProductDetailsByUrlAsync(url);
            return _mapper.Map<ProductDTO?>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsWithCategoryAsync()
        {
           var products = await _productRepository.GetProductsWithCategoryAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<int> GetSearchCountAsync(string keyword)
        {
            return await _productRepository.GetSearchCountAsync(keyword);
        }

        public async Task<IEnumerable<ProductDTO>> GetSearchProductAsync(string keyword, int page, int pageSize)
        {
           var products = await _productRepository.GetSearchProductAsync(keyword, page, pageSize);
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<bool> IsInStockAsync(int ProductId)
        {
            return await _productRepository.IsInStockAsync(ProductId);
        }

        public async Task UpdateProductAsync(UpdateProductDTO productdto)
        {
            var product = _mapper.Map<Product>(productdto);
            await _productRepository.UpdateAsync(product);
            await _productRepository.UpdateProductCategoriesAsync(product.ProductID,productdto.CategoryIDs);
        }

        public async Task UpdateStockAsync(int ProductId, int quantity)
        {
            await _productRepository.UpdateStockAsync(ProductId, quantity);
        }
    }
}
