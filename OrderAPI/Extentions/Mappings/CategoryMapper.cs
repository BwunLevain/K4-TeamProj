using OrderAPI.Features.Categories;
using OrderAPI.Features.TimeLogs;

namespace OrderAPI.Extentions.Mappings
{
    public static class CategoryMapper
    {
        public static CategoryResponse ToResponse(this Category category)
        {
            return new CategoryResponse(
                category.Id,
                category.Name,
                category.TimeLogs.Select(tl => tl.ToResponse()).ToList()
            );
        }

        public static Category ToEntity(this CreateCategoryRequest request)
        {
            return new Category
            {
                Name = request.Name
            };
        }
    }
}
