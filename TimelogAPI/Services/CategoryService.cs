using TimelogAPI.Features.Categories;
using TimelogAPI.Features.Categories.Dtos;
using TimelogAPI.Features.Common;
using TimelogAPI.Extentions;
using TimelogAPI.Extentions.Mappers;
using Microsoft.Extensions.Caching.Hybrid;

namespace TimelogAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly HybridCache _cache;

        public static readonly List<Category> _categories = Enumerable.Range(1, 3).Select(i => new Category
        {
            Id = i,
            Name = $"Category {i}"
        }).ToList();

        private static int _nextId = 4;

        public CategoryService(ILogger<CategoryService> logger, HybridCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public async Task<PagedResponseDto<CategoryResponse>> GetPagedCategoriesAsync(int page, int pageSize, string? searchTerm)
        {
            var query = _categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            return query
                .OrderBy(c => c.Name)
                .ToPagedResponse(page, pageSize, c => c.ToResponse());
        }

        public async Task<CategoryResponse> GetCategoryByIdAsync(int id)
        {
            return await _cache.GetOrCreateAsync(
                $"category-{id}",
                async _ =>
                {
                    var category = _categories.FirstOrDefault(c => c.Id == id);
                    return category?.ToResponse()!;
                }
            );
        }

        public async Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest request)
        {
            var entity = request.ToEntity();
            entity.Id = _nextId++;

            _categories.Add(entity);

            _logger.LogInformation("Created new category: {CategoryName}", entity.Name);
            return entity.ToResponse();
        }

        public async Task<bool> UpdateCategoryAsync(int id,UpdateCategoryRequest request)
        {
            var existing = _categories.FirstOrDefault(c => c.Id == id);
            if (existing == null) return false;

            request.UpdateEntity(existing);

            _logger.LogInformation("Updated category ID: {CategoryId}", id);
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var existing = _categories.FirstOrDefault(c => c.Id == id);

            if (existing == null) return false;

            _categories.Remove(existing);

            _logger.LogWarning("Deleted category ID: {CategoryId}", id);
            return true;
        }
    }
}