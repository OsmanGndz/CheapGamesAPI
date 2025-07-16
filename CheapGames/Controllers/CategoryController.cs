using Microsoft.AspNetCore.Mvc;
using CheapGames.Mappers;
using CheapGames.Interfaces;
using CheapGames.Dtos.Category;
using CheapGames.Dtos.Game;


namespace CheapGames.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepo.GetAllCategoriesAsync();
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
            }

            var dto = categories.Select(c => c.ToCategoryReadDto()).ToList();

            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _categoryRepo.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            var dto = category.ToCategoryReadDto();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (category == null)
            {
                return BadRequest("Category data is null.");
            }

            var newCategory = category.ToCategoryCreateDto();
            await _categoryRepo.CreateCategoryAsync(newCategory);

            var categoryReadDto = newCategory.ToCategoryReadDto(); // Convert the created category to CategoryReadDto  
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.Id }, categoryReadDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryUpdateDto categoryDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (categoryDto == null)
            {
                return BadRequest("Category data is null.");
            }

            var updatedCategory = await _categoryRepo.UpdateCategoryAsync(id, categoryDto);
            if (updatedCategory == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            var dto = updatedCategory.ToCategoryReadDto();
            return Ok(dto);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var deletedCategory = await _categoryRepo.DeleteCategoryAsync(id);
            if (deletedCategory == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }
            return Ok(deletedCategory.ToCategoryReadDto());

        }
    }
}
