using TimelogAPI.Features.Categories.Dtos;
using TimelogAPI.Features.Common;

namespace TimelogAPI.Services
{
    public interface ICategoryService
    {
        Task<PagedResponseDto<CategoryResponse>> GetPagedCategoriesAsync(int page, int pageSize, string? searchTerm);

        Task<CategoryResponse> GetCategoryByIdAsync(int id);

        Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest request);

        Task<bool> UpdateCategoryAsync(int id,UpdateCategoryRequest request);

        Task<bool> DeleteCategoryAsync(int id);
    }
}