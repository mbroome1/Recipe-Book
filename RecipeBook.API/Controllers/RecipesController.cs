using Microsoft.AspNetCore.Mvc;
using RecipeBook.Core.Entities;
using RecipeBook.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeSearchData recipeSearchData;
        private readonly IRecipeData recipeData;

        public RecipeSearch RecipeSearchResult { get; set; }
        public Recipe RecipeResult { get; set; }

        public RecipesController(IRecipeSearchData recipeSearchData, IRecipeData recipeData)
        {
            this.recipeSearchData = recipeSearchData;
            this.recipeData = recipeData;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes([FromQuery] string searchQuery)
        {
            // Return 400 Bad Request if search is empty or white space
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return BadRequest(new { message = "Search must not be empty."});
            }
            try
            {
                RecipeSearchResult = await recipeSearchData.Get(searchQuery);
                return Ok(RecipeSearchResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message});
                
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            RecipeResult = await recipeData.Get(id);
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            return Ok(RecipeResult);
        }
    }
}
