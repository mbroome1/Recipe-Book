using RecipeBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data
{
    public interface IRecipeData
    {
        Task<Recipe> Get(int id);
    }
}
