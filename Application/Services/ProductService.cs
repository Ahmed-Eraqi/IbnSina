using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<ProductListItemDto>> GetAllAsync(string? search, int? categoryId)
        {
            List<Product> products = await _productRepository.GetActiveAsync(search, categoryId);
            return products.Select(ToListItem).ToList();
        }

        public async Task<ProductDetailDto?> GetByIdAsync(int id)
        {
            Product? product = await _productRepository.GetActiveByIdAsync(id);
            if (product == null)
                return null;

            return ToDetail(product);
        }

        public async Task<ProductDetailDto> CreateAsync(ProductInputDto input)
        {
            Category category = await GetCategoryOrThrowAsync(input.CategoryId);

            Product product = new Product
            {
                Name = input.Name,
                Description = input.Description,
                Price = input.Price,
                Stock = input.Stock,
                CategoryId = category.Id,
                Category = category,
                IsActive = true
            };

            await _productRepository.AddAsync(product);
            return ToDetail(product);
        }

        public async Task<ProductDetailDto?> UpdateAsync(int id, ProductInputDto input)
        {
            Product? product = await _productRepository.GetActiveByIdAsync(id);
            if (product == null)
                return null;

            Category category = await GetCategoryOrThrowAsync(input.CategoryId);

            product.Name = input.Name;
            product.Description = input.Description;
            product.Price = input.Price;
            product.Stock = input.Stock;
            product.CategoryId = category.Id;
            product.Category = category;

            await _productRepository.SaveChangesAsync();
            return ToDetail(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Product? product = await _productRepository.GetActiveByIdAsync(id);
            if (product == null)
                return false;

            product.IsActive = false;   // set isactive to false
            await _productRepository.SaveChangesAsync();
            return true;
        }

        private async Task<Category> GetCategoryOrThrowAsync(int categoryId)
        {
            Category? category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
                throw new ArgumentException("This category does not exist.");

            return category;
        }

        private static ProductListItemDto ToListItem(Product product)
        {
            return new ProductListItemDto(
                product.Id,
                product.Name,
                product.Price,
                product.Stock,
                product.Category?.Name ?? "",
                product.CategoryId);
        }

        private static ProductDetailDto ToDetail(Product product)
        {
            return new ProductDetailDto(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Stock,
                product.Category?.Name ?? "",
                product.CategoryId);
        }
    }
}

