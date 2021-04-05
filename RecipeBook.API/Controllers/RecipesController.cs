using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.API.Models;
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
        private readonly IMapper mapper;

        public RecipeSearch RecipeSearchResult { get; set; }
        public Recipe RecipeResult { get; set; }

        public RecipesController(IRecipeSearchData recipeSearchData, IRecipeData recipeData, IMapper mapper)
        {
            this.recipeSearchData = recipeSearchData;
            this.recipeData = recipeData;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes(
            [FromQuery] string searchQuery, 
            int? numberOfRecords = 10, 
            int? offset = 0)
        {
            // Return 400 Bad Request if search is empty or white space
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return StatusCode(400, new { message = "Search must not be empty."});
            }
            try
            {
                RecipeSearchResult = await recipeSearchData.Get(searchQuery,numberOfRecords,offset);
                return Ok(mapper.Map<RecipeSearchDto>(RecipeSearchResult));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message});
                
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            try
            {
                RecipeResult = await recipeData.Get(id);
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                return Ok(mapper.Map<RecipeDto>(RecipeResult));
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
