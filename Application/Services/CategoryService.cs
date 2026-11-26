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
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            List<Category> categories = await _categoryRepository.GetAllAsync();
            return categories.Select(ToDto).ToList();
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            Category? category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return null;

            return ToDto(category);
        }

        public async Task<CategoryDto> CreateAsync(CategoryInputDto input)
        {
            if (await _categoryRepository.NameExistsAsync(input.Name, null))
                throw new InvalidOperationException("A category with this name already exists.");

            Category category = new Category
            {
                Name = input.Name,
                Description = input.Description
            };

            await _categoryRepository.AddAsync(category);
            return ToDto(category);
        }

        public async Task<CategoryDto?> UpdateAsync(int id, CategoryInputDto input)
        {
            Category? category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return null;

            if (await _categoryRepository.NameExistsAsync(input.Name, id))
                throw new InvalidOperationException("A category with this name already exists.");

            category.Name = input.Name;
            category.Description = input.Description;

            await _categoryRepository.SaveChangesAsync();
            return ToDto(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Category? category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return false;

            if (await _categoryRepository.HasProductsAsync(id))
                throw new InvalidOperationException("This category still has products. Move or remove them first.");

            await _categoryRepository.DeleteAsync(category);
            return true;
        }

        private static CategoryDto ToDto(Category category)
        {
            return new CategoryDto(category.Id, category.Name, category.Description);
        }
    }
}


