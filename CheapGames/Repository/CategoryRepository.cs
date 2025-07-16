using CheapGames.Interfaces;
using CheapGames.Models;
using CheapGames.Data;
using Microsoft.EntityFrameworkCore;
using CheapGames.Dtos.Category;

namespace CheapGames.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;
        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteCategoryAsync(int id)
        {
            var existCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            if (existCategory == null)
            {
                return null;
            }
            _context.Categories.Remove(existCategory);
            _context.SaveChanges();
            return existCategory;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(int id, CategoryUpdateDto category)
        {
            var existedCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            if (existedCategory == null)
            {
                return null;
            }
            existedCategory.CategoryName = category.CategoryName;
            await _context.SaveChangesAsync();

            return existedCategory;
        }
    }
}
