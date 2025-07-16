using CheapGames.Models;
using CheapGames.Dtos.Category;
namespace CheapGames.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category?> UpdateCategoryAsync(int id, CategoryUpdateDto category);
        Task<Category?> DeleteCategoryAsync(int id);
    }
}
