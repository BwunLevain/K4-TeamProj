using System.Collections.Generic;
using TimelogAPI.Features.Categories;
using TimelogAPI.Features.Categories.Dtos;
using TimelogAPI.Features.TimeLogs.Dtos;

namespace TimelogAPI.Extentions.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryResponse ToResponse(this Category category)
        {
            if (category == null) return null!;

            return new CategoryResponse(
                category.Id,
                category.Name,
                new List<TimeLogResponse>()
            );
        }

        public static Category ToEntity(this CreateCategoryRequest request)
        {
            if (request == null) return null!;

            return new Category
            {
                Name = request.Name
            };
        }

        public static void UpdateEntity(this UpdateCategoryRequest request, Category existingCategory)
        {
            if (request == null || existingCategory == null) return;

            existingCategory.Name = request.Name;
        }
    }
}