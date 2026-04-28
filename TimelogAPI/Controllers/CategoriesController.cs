using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimelogAPI.Features.Categories.Dtos;
using TimelogAPI.Services;

namespace TimelogAPI.Controllers
{
    /// <summary>
    /// Hanterar kategorier för tidrapportering.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Skapar en ny kategori.
        /// </summary>
        /// <param name="request">Data för den nya kategorin.</param>
        /// <returns>Den nyskapade kategorin.</returns>
        /// <response code="201">Returnerar den nyskapade kategorin.</response>
        /// <response code="400">Om förfrågan är felaktig.</response>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            var result = await _categoryService.CreateCategoryAsync(request);
            return CreatedAtAction(nameof(GetCategoryById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Hämtar en specifik kategori baserat på ID.
        /// </summary>
        /// <param name="id">Kategorins unika identifierare.</param>
        /// <returns>En kategori.</returns>
        /// <response code="200">Returnerar den efterfrågade kategorin.</response>
        /// <response code="404">Om kategorin inte hittades.</response>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Hämtar en sökbar och siduppdelad lista av kategorier.
        /// </summary>
        /// <param name="page">Sidnummer (standardvärde 1).</param>
        /// <param name="pageSize">Antal resultat per sida (standardvärde 20).</param>
        /// <param name="searchTerm">Frivillig sökterm för att filtrera kategorier på namn.</param>
        /// <returns>En lista med kategorier.</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCategories(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? searchTerm = null)
        {
            var result = await _categoryService.GetPagedCategoriesAsync(page, pageSize, searchTerm);
            return Ok(result);
        }

        /// <summary>
        /// Uppdaterar en befintlig kategori.
        /// </summary>
        /// <param name="id">ID för kategorin som ska uppdateras.</param>
        /// <param name="request">Den uppdaterade datan.</param>
        /// <returns>Inget innehåll vid lyckad uppdatering.</returns>
        /// <response code="204">Kategorin har uppdaterats.</response>
        /// <response code="404">Om kategorin inte existerar.</response>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryRequest request)
        {
            await _categoryService.UpdateCategoryAsync(id, request);
            return NoContent();
        }

        /// <summary>
        /// Tar bort en kategori.
        /// </summary>
        /// <param name="id">ID för kategorin som ska raderas.</param>
        /// <returns>Inget innehåll vid lyckad borttagning.</returns>
        /// <response code="204">Kategorin har raderats.</response>
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}