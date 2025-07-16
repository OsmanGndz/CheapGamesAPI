using CheapGames.Models;
using CheapGames.Dtos.Category;

namespace CheapGames.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryReadDto ToCategoryReadDto(this Category category)
        {
            if (category == null) return null;
            return new CategoryReadDto
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };
        }

        public static Category ToCategoryCreateDto(this CategoryCreateDto category)
        {
            if (category == null) return null;
            return new Category
            {
                CategoryName = category.CategoryName,
            };
        }

        public static Category ToCategoryUpdateDto(this CategoryUpdateDto category)
        {
            if (category == null) return null;
            return new Category
            {
                CategoryName = category.CategoryName,
            };
        }
    }
}
